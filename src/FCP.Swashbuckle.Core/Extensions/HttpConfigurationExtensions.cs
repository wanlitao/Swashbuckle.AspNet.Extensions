using Swashbuckle.Application;

namespace System.Web.Http
{
    public static class HttpConfigurationExtensions
    {
        public static HttpConfiguration EnableSwaggerAsHomePage(this HttpConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            config.Routes.MapHttpRoute(
                name: "swagger_root",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "swagger")
            );

            return config;
        }
    }
}
