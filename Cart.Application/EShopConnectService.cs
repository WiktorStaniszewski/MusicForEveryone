using Cart.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cart.Application;

public class EShopConnectService : IEShopConnectService
{
    private readonly HttpClient _httpClient;
    public EShopConnectService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductGetDTO> GetProductAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Product/{id}");  //error "The response ended prematurely. (ResponseEnded)"
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductGetDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex) { throw ex; }
    }
}
