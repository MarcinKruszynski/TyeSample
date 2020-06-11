using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Model;

namespace WebApp.Clients
{
    public class ProductsClient
     {
         private readonly JsonSerializerOptions options = new JsonSerializerOptions()
         {
             PropertyNameCaseInsensitive = true,
             PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
         };
 
         private readonly HttpClient client;
 
         public ProductsClient(HttpClient client)
         {
             this.client = client;
         }
 
         public async Task<ProductItem[]> GetProductsAsync()
         {
             var responseMessage = await this.client.GetAsync("/products");
             var stream = await responseMessage.Content.ReadAsStreamAsync();
             return await JsonSerializer.DeserializeAsync<ProductItem[]>(stream, options);
         }
     }
}