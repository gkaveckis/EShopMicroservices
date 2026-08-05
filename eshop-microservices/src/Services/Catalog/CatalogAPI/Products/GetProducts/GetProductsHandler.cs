using Marten;

namespace CatalogAPI.Products.GetProducts
{
    public record GetProductsQuery(): IQuery<GetProductsResult>;   
    public record GetProductsResult(IEnumerable<Product> Products);
    public class GetProductsQueryHandler(IQuerySession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
