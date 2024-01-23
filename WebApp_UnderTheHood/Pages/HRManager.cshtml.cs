using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_UnderTheHood.DTO;

namespace WebApp_UnderTheHood.Pages;

[Authorize(Policy = "HRManagerOnly")]
public class HRManagerModel : PageModel
{
    private readonly IHttpClientFactory httpClientFactory;

    [BindProperty]
    public List<WeatherForecastDTO> WeatherForecastItems { get; set; } = new();

    public HRManagerModel(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task OnGetAsync()
    {

        var httpClient = httpClientFactory.CreateClient("WeatherAPI");
        WeatherForecastItems = await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast")??new List<WeatherForecastDTO>();
    }

}   
