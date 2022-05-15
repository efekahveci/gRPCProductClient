// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using ProductProto;

using var channel = GrpcChannel.ForAddress("http://localhost:5128");
var client = new Product.ProductClient(channel);



//Unary gRPC Example
//GetProductsRequest req = new GetProductsRequest { ProductId = 1 };
//var reply = client.GetProduct(req);


//Console.WriteLine(reply);


//Console.ReadLine();


while (true)
{
    Console.Clear();
    Console.WriteLine("\n1-GetProduct => ID'ye göre ürün arama.");
    Console.WriteLine("\n2-GetAllProducts => Tüm ürünleri getir.");
    Console.WriteLine("\n3-SearchProduct => İsme göre ürün arama.");
    Console.WriteLine("\n0. EXIT !");
    Console.Write("\nLütfen seçim yapınız.");
    var choice = Console.ReadKey();
    Console.Clear();

    switch (choice.KeyChar)
    {
        case '1':
            Console.WriteLine("\nLütfen sayı giriniz..");
            GetProductsRequest getProductsRequest = new GetProductsRequest { ProductId = Convert.ToInt32(Console.ReadLine()) };
            var productreply = client.GetProduct(getProductsRequest);
            Console.WriteLine("Cevap:\n");
            Console.WriteLine(productreply);
            break;
        case '2':
            Console.WriteLine("\nTüm ürünler:");
            GetAllRequest getAllRequest = new GetAllRequest();
            var getAllReply = await client.GetAllProductsAsync(getAllRequest);
            Console.WriteLine("Cevap:\n");
            var result = getAllReply.Product as IList<ProductModel>;
            Console.WriteLine(result);
            break;
        case '3':
            Console.WriteLine("\nLütfen isim giriniz..");
            SearchRequest searchRequest = new SearchRequest { ProductName = Console.ReadLine() };
            var searchreply = client.SearchProducts(searchRequest);
            while (await searchreply.ResponseStream.MoveNext())
            {
                Console.WriteLine(searchreply.ResponseStream.Current);
            } 
            Console.WriteLine("Cevap:\n");
        
            break;
        case '0':
            Console.WriteLine();
            Console.WriteLine("bye bye");
            await Task.Delay(2000);
            Environment.Exit(0);
            break;
        default:
            break;
    }
    Console.ReadLine();
}