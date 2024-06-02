using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

public class FileUploadOperation : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileUploadParams = context.ApiDescription.ParameterDescriptions
            .Where(p => p.ModelMetadata.ContainerType == typeof(IFormFile))
            .ToList();

        if (!fileUploadParams.Any()) return;

        foreach (var param in fileUploadParams)
        {
            operation.Parameters.Remove(operation.Parameters.First(p => p.Name == param.Name));
        }

        operation.RequestBody = new OpenApiRequestBody
        {
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties = fileUploadParams.ToDictionary(p => p.Name, p => new OpenApiSchema
                        {
                            Type = "string",
                            Format = "binary"
                        })
                    }
                }
            }
        };
    }
}