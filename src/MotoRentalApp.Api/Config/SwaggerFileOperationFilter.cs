using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MotoRentalApp.Api.Config
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var parameter in operation.Parameters)
            {
                if (context.ApiDescription.ActionDescriptor.Parameters.Any(p => p.Name == parameter.Name && p.ParameterType == typeof(IFormFile)))
                {
                    operation.RequestBody = new OpenApiRequestBody
                    {
                        Content = new Dictionary<string, OpenApiMediaType>
                        {
                            ["multipart/form-data"] = new OpenApiMediaType
                            {
                                Schema = new OpenApiSchema
                                {
                                    Type = "object",
                                    Properties =
                                    {
                                        [parameter.Name] = new OpenApiSchema
                                        {
                                            Type = "string",
                                            Format = "binary"
                                        }
                                    },
                                    Required = new HashSet<string> { parameter.Name }
                                }
                            }
                        }
                    };

                    operation.Parameters.Remove(parameter);
                }
            }
        }
    }
}