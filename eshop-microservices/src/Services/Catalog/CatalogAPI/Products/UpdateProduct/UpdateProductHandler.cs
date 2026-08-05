using System.Reflection.Metadata;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Product Id is required.");
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Product name is required.")
                .Length(2, 150)
                .WithMessage("Product name must be between 2 and 150 characters.");
         
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be a positive value.");
        }
    }

    internal class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(command.id);
            }

            product.Name = command.Name;
            product.Categories = command.Categories;
            product.Description = command.Description;
            product.ImageFile= command.ImageFile;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }        
    }
}
