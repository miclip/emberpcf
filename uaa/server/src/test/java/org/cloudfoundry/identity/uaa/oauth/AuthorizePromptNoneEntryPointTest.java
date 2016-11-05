/*
 * ****************************************************************************
 *     Cloud Foundry
 *     Copyright (c) [2009-2016] Pivotal Software, Inc. All Rights Reserved.
 *
 *     This product is licensed to you under the Apache License, Version 2.0 (the "License").
 *     You may not use this product except in compliance with the License.
 *
 *     This product includes a number of subcomponents with
 *     separate copyright notices and license terms. Your use of these
 *     subcomponents is subject to the terms and conditions of the
 *     subcomponent's license, as noted in the LICENSE file.
 * ****************************************************************************
 */

package org.cloudfoundry.identity.uaa.oauth;

import org.junit.Before;
import org.junit.Test;
import org.springframework.http.HttpStatus;
import org.springframework.mock.web.MockHttpServletRequest;
import org.springframework.mock.web.MockHttpServletResponse;
import org.springframework.security.oauth2.common.exceptions.RedirectMismatchException;
import org.springframework.security.oauth2.common.util.OAuth2Utils;
import org.springframework.security.oauth2.provider.ClientDetailsService;
import org.springframework.security.oauth2.provider.NoSuchClientException;
import org.springframework.security.oauth2.provider.client.BaseClientDetails;
import org.springframework.security.oauth2.provider.endpoint.RedirectResolver;
import org.springframework.security.web.AuthenticationEntryPoint;
import org.springframework.security.web.authentication.AuthenticationFailureHandler;
import org.springframework.security.web.authentication.session.SessionAuthenticationException;

import java.util.Collections;

import static org.junit.Assert.assertEquals;
import static org.mockito.Matchers.anyString;
import static org.mockito.Matchers.eq;
import static org.mockito.Matchers.same;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.reset;
import static org.mockito.Mockito.times;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

public class AuthorizePromptNoneEntryPointTest {

    public static final String REDIRECT_URI = "http://sub.domain.com/callback?oauth=true";
    public static final String HTTP_SOME_OTHER_SITE_CALLBACK = "http://some.other.site/callback";
    private final SessionAuthenticationException authException = new SessionAuthenticationException("");
    private AuthenticationFailureHandler failureHandler;
    private AuthenticationEntryPoint entryPoint;
    private BaseClientDetails client;
    private MockHttpServletRequest request;
    private MockHttpServletResponse response;
    private ClientDetailsService clientDetailsService;
    private RedirectResolver redirectResolver;

    @Before
    public void setup() {
        client = new BaseClientDetails("id", "", "openid", "authorization_code", "", REDIRECT_URI);
        clientDetailsService = mock(ClientDetailsService.class);
        redirectResolver = mock(RedirectResolver.class);
        failureHandler = mock(AuthenticationFailureHandler.class);

        when(clientDetailsService.loadClientByClientId(eq(client.getClientId()))).thenReturn(client);
        when(redirectResolver.resolveRedirect(eq(REDIRECT_URI), same(client))).thenReturn(REDIRECT_URI);
        when(redirectResolver.resolveRedirect(eq(HTTP_SOME_OTHER_SITE_CALLBACK), same(client))).thenThrow(new RedirectMismatchException(""));

        entryPoint = new AuthorizePromptNoneEntryPoint(failureHandler, clientDetailsService, redirectResolver);

        request = new MockHttpServletRequest("GET", "/oauth/authorize");
        request.setParameter(OAuth2Utils.CLIENT_ID, client.getClientId());
        response = new MockHttpServletResponse();
    }

    @Test
    public void test_missing_client_id() throws Exception {
        request.removeParameter(OAuth2Utils.CLIENT_ID);
        entryPoint.commence(request, response, authException);
        assertEquals(HttpStatus.BAD_REQUEST.value(), response.getStatus());
    }

    @Test
    public void test_missing_redirect_uri() throws Exception {
        client.setRegisteredRedirectUri(Collections.emptySet());
        entryPoint.commence(request, response, authException);
        assertEquals(HttpStatus.BAD_REQUEST.value(), response.getStatus());
    }

    @Test
    public void test_client_not_found() throws Exception {
        reset(clientDetailsService);
        when(clientDetailsService.loadClientByClientId(anyString())).thenThrow(new NoSuchClientException("not found"));
        entryPoint.commence(request, response, authException);
        assertEquals(HttpStatus.BAD_REQUEST.value(), response.getStatus());
    }

    @Test
    public void test_redirect_mismatch() throws Exception {
        request.setParameter(OAuth2Utils.REDIRECT_URI, HTTP_SOME_OTHER_SITE_CALLBACK);
        entryPoint.commence(request, response, authException);
        assertEquals(HttpStatus.BAD_REQUEST.value(), response.getStatus());
    }

    @Test
    public void test_redirect_contains_error() throws Exception {
        request.setParameter(OAuth2Utils.REDIRECT_URI, REDIRECT_URI);
        entryPoint.commence(request, response, authException);
        assertEquals(HttpStatus.FOUND.value(), response.getStatus());
        assertEquals(REDIRECT_URI+"&error=login_required", response.getHeader("Location"));
    }

    @Test
    public void test_failure_handler_is_invoked() throws Exception {
        request.setParameter(OAuth2Utils.REDIRECT_URI, REDIRECT_URI);
        entryPoint.commence(request, response, authException);
        verify(failureHandler, times(1)).onAuthenticationFailure(same(request), same(response), same(authException));
    }



}