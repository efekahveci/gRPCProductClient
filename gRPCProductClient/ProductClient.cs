using Grpc.Core;
using Grpc.Net.Client;
using ProductProto;

namespace gRPCProductClient
{
    internal class ProductClient
    {

        private GrpcChannel grpcChannel;
        private Product.ProductClient productClient;

        public ProductClient()
        {
            grpcChannel = GrpcChannel.ForAddress("http://localhost:5128");
            productClient = new Product.ProductClient(grpcChannel);
        }

        internal ProductModel GetProduct(int id)
        {
            GetProductsRequest getProductsRequest = new GetProductsRequest { ProductId = id };
            var result = productClient.GetProduct(getProductsRequest);

            return result;
        }

        internal async Task<IList<ProductModel>> GetAllProducts()
        {
            GetAllRequest getAllRequest = new GetAllRequest();
            var getAllReply = await productClient.GetAllProductsAsync(getAllRequest);
            var result = getAllReply.Product as IList<ProductModel>;

            return result;
        }

        internal async Task SearchProduct(string name)
        {
            var token = new CancellationTokenSource();
            SearchRequest searchRequest = new SearchRequest { ProductName = name };
            var searchreply = productClient.SearchProducts(searchRequest);

            try
            {
                await foreach (var response in searchreply.ResponseStream.ReadAllAsync(token.Token))
                {

                    Console.WriteLine(response.ProductName);

                }
                Console.WriteLine("Tüm ürünler çekildi.");

                token.Cancel();

            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
