
using Grpc.Core;
using Grpc.Net.Client;
using gRPCProductClient;
using ProductProto;


ProductClient productClient = new ProductClient(); 

while (true)
{
    Console.Clear();

    Console.WriteLine("\n1-GetProduct => ID'ye göre ürün arama.\n2-GetAllProducts => Tüm ürünleri getir.\n3-SearchProduct => İsme göre ürün arama.\nLütfen seçim yapınız.");

    var choice = Console.ReadKey();
    Console.Clear();

    switch (choice.KeyChar)
    {
        case '1':
            Console.WriteLine("Unary Call\n");
            Console.WriteLine(productClient.GetProduct(2));
            break;
        case '2':
            Console.WriteLine("Unary Call\n");
            Console.WriteLine(await productClient.GetAllProducts());      
            break;
        case '3':
            Console.WriteLine("Server Streaming Call\n");
            await productClient.SearchProduct("a");
            break;
        default:
            break;
    }
    Console.ReadLine();
}