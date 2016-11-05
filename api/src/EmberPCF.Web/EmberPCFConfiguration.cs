using EmberPCF.Web.Controllers;
using EmberPCF.Web.Models;
using NJsonApi.Web.MVCCore;
using NJsonApi.Web;
using NJsonApi;

namespace EmberPCF.Web
{
    public static class NJsonApiConfiguration
    {
        public static NJsonApi.IConfiguration BuildConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();

            configBuilder
                .Resource<Article, ArticlesController>()
                .WithAllProperties();

            configBuilder
                .Resource<Person, PeopleController>()
                .WithAllProperties();

            configBuilder
                .Resource<Comment, CommentsController>()
                .WithAllProperties();

            var nJsonApiConfig = configBuilder.Build();
            return nJsonApiConfig;
        }
    }
}