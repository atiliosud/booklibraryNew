using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

// If the class is meant to be internal, ensure it is marked as such and named appropriately
internal class ODataSwaggerOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Modify operation based on OData specifics, e.g., handling $expand, $filter, etc.
        // You can add logic here to adjust the OpenApiOperation based on the needs of OData endpoints
    }
}
