using BuildingBlocks.CQRS;
using CatalogApi.Models;

namespace CatalogApi.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Category, string Description,string ImageFile, decimal Price) 
        : ICommand<CreateProductResult> ;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommandHandler (IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Create product entity from command object
            //save to result 
            //return Create product result

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price

            };
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }
    }
}
