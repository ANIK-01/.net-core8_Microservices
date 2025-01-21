
using CatalogApi.Products.GetProduct;

namespace CatalogApi.Products.CreateProduct
{

    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint(ILogger<CreateProductEndpoint> logger) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products",
                async (CreateProductRequest request, ISender sender) =>
                 {
                    var command = request.Adapt<CreateProductCommand>();
                     logger.LogInformation("CreateProduct {@request.Name}.Handle called with {@command}", command);

                     var result = await sender.Send(command);
                     logger.LogInformation("CreateProduct {@request.Name}.Handle called with {@result}", result);

                     var response = result.Adapt<CreateProductResponse>();
                    return Results.Created($"/products/{response.Id}", response);
                })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product");

        }
    }
}
