using System;
using System.Linq;
using System.Web.Http.Description;

namespace Swashbuckle.Swagger
{
    public class RemoveVersionParameters : IOperationFilter
    {
        private readonly string _versionParameterName;

        public RemoveVersionParameters(string versionParamName)
        {
            if (string.IsNullOrWhiteSpace(versionParamName))
                throw new ArgumentNullException(nameof(versionParamName));

            _versionParameterName = versionParamName;
        }

        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            // Remove version parameter from all Operations
            var versionParameter = operation.parameters.SingleOrDefault(p => string.Compare(p.name, _versionParameterName, true) == 0);

            if (versionParameter != null)
            {
                operation.parameters.Remove(versionParameter);
            }
        }
    }
}
