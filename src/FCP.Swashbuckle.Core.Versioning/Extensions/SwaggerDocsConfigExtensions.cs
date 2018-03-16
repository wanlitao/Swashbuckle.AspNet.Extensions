using Microsoft.Web.Http.Description;
using Swashbuckle.Swagger;
using System;
using System.Web.Http.Description;

namespace Swashbuckle.Application
{
    public static class SwaggerDocsConfigExtensions
    {
        public static SwaggerDocsConfig VersioningSwaggerDoc(this SwaggerDocsConfig config,
            VersionedApiExplorer apiExplorer)
        {
            return VersioningSwaggerDoc(config, apiExplorer, "API {0}");
        }

        public static SwaggerDocsConfig VersioningSwaggerDoc(this SwaggerDocsConfig config,
            VersionedApiExplorer apiExplorer, string docTitleFormat)
        {
            return VersioningSwaggerDoc(config, apiExplorer, docTitleFormat, "api-version");
        }

        public static SwaggerDocsConfig VersioningSwaggerDoc(this SwaggerDocsConfig config,
            VersionedApiExplorer apiExplorer, string docTitleFormat, string versionParamName)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            if (apiExplorer == null)
                throw new ArgumentNullException(nameof(apiExplorer));

            if (string.IsNullOrWhiteSpace(docTitleFormat))
                throw new ArgumentNullException(nameof(docTitleFormat));

            if (string.IsNullOrWhiteSpace(versionParamName))
                throw new ArgumentNullException(nameof(versionParamName));

            config.MultipleApiVersions(
                        (apiDescription, version) => apiDescription.GetGroupName() == version,
                        info =>
                        {
                            foreach (var group in apiExplorer.ApiDescriptions)
                            {
                                info.Version(group.Name, string.Format(docTitleFormat, group.ApiVersion));
                            }
                        });

            config.OperationFilter(() => new RemoveVersionParameters(versionParamName));
            config.DocumentFilter(() => new SetVersionInPaths(versionParamName));

            return config;
        }
    }
}
