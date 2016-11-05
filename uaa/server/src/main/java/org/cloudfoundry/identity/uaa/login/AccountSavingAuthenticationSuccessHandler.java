package org.cloudfoundry.identity.uaa.login;

import org.cloudfoundry.identity.uaa.authentication.UaaPrincipal;
import org.cloudfoundry.identity.uaa.util.JsonUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.Authentication;
import org.springframework.security.web.authentication.AuthenticationSuccessHandler;
import org.springframework.security.web.authentication.SavedRequestAwareAuthenticationSuccessHandler;

import javax.servlet.ServletException;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import java.nio.charset.StandardCharsets;

import static java.nio.charset.StandardCharsets.UTF_8;

public class AccountSavingAuthenticationSuccessHandler implements AuthenticationSuccessHandler {

    @Autowired
    public SavedRequestAwareAuthenticationSuccessHandler redirectingHandler;

    public SavedRequestAwareAuthenticationSuccessHandler getRedirectingHandler() {
        return redirectingHandler;
    }

    public void setRedirectingHandler(SavedRequestAwareAuthenticationSuccessHandler redirectingHandler) {
        this.redirectingHandler = redirectingHandler;
    }

    @Override
    public void onAuthenticationSuccess(HttpServletRequest request, HttpServletResponse response, Authentication authentication) throws IOException, ServletException {
        setSavedAccountOptionCookie(request, response, authentication);
        redirectingHandler.onAuthenticationSuccess(request, response, authentication);
    }

    public void setSavedAccountOptionCookie(HttpServletRequest request, HttpServletResponse response, Authentication authentication) throws IllegalArgumentException {
        Object principal = authentication.getPrincipal();

        if(!(principal instanceof UaaPrincipal)) {
            throw new IllegalArgumentException("Unrecognized authentication principle.");
        }

        UaaPrincipal uaaPrincipal = (UaaPrincipal) principal;
        SavedAccountOption savedAccountOption = new SavedAccountOption();
        savedAccountOption.setEmail(uaaPrincipal.getEmail());
        savedAccountOption.setOrigin(uaaPrincipal.getOrigin());
        savedAccountOption.setUserId(uaaPrincipal.getId());
        savedAccountOption.setUsername(uaaPrincipal.getName());
        Cookie savedAccountCookie = new Cookie("Saved-Account-" + uaaPrincipal.getId(), JsonUtils.writeValueAsString(savedAccountOption));

        savedAccountCookie.setPath(request.getContextPath() + "/login");
        savedAccountCookie.setHttpOnly(true);
        savedAccountCookie.setSecure(request.isSecure());
        // cookie expires in a year
        savedAccountCookie.setMaxAge(365*24*60*60);

        response.addCookie(savedAccountCookie);

        CurrentUserInformation currentUserInformation = new CurrentUserInformation();
        currentUserInformation.setUserId(uaaPrincipal.getId());
        Cookie currentUserCookie;
        try {
            currentUserCookie = new Cookie("Current-User", URLEncoder.encode(JsonUtils.writeValueAsString(currentUserInformation), UTF_8.name()));
        } catch (UnsupportedEncodingException e) {
            throw new IllegalArgumentException(e);
        }
        currentUserCookie.setMaxAge(365*24*60*60);
        currentUserCookie.setHttpOnly(false);

        response.addCookie(currentUserCookie);
    }
}
