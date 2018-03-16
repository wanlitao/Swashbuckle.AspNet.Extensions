using System;
using System.Linq;
using System.Web.Http.Description;

namespace Swashbuckle.Swagger
{
    public class SetVersionInPaths : IDocumentFilter
    {
        private readonly string _versionParameterName;
        
        public SetVersionInPaths(string versionParamName)
        {
            if (string.IsNullOrWhiteSpace(versionParamName))
                throw new ArgumentNullException(nameof(versionParamName));

            _versionParameterName = versionParamName;
        }

        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths = swaggerDoc.paths
                .ToDictionary(
                    path => path.Key.Replace($"v{{{_versionParameterName}}}", swaggerDoc.info.version),
                    path => path.Value
                );
        }
    }
}
