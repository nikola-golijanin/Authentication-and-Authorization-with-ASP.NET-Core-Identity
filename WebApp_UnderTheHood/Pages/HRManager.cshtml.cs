using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApp_UnderTheHood.Authorization;
using WebApp_UnderTheHood.DTO;
using WebApp_UnderTheHood.Pages.Account;

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
        // get token from session
            JwtToken token = new JwtToken();

        var strTokenObj = HttpContext.Session.GetString("access_token");
        if (string.IsNullOrEmpty(strTokenObj))
        {
            token = await Authenticate();
        }
        else
        {
            token = JsonConvert.DeserializeObject<JwtToken>(strTokenObj)??new JwtToken();
        }

        if (token == null ||
            string.IsNullOrWhiteSpace(token.AccessToken) ||
            token.ExpiresAt <= DateTime.UtcNow)
        {
            token = await Authenticate();
        }

        var httpClient = httpClientFactory.CreateClient("WeatherAPI");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken??string.Empty);
        WeatherForecastItems = await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast")??new List<WeatherForecastDTO>();
    }

    private async  Task<JwtToken> Authenticate()
    {            
        var httpClient = httpClientFactory.CreateClient("WeatherAPI");
        var res = await httpClient.PostAsJsonAsync("auth", new Credential { UserName = "admin", Password = "admin" });
        res.EnsureSuccessStatusCode();
        string strJwt = await res.Content.ReadAsStringAsync();
        HttpContext.Session.SetString("access_token", strJwt);

        return JsonConvert.DeserializeObject<JwtToken>(strJwt) ?? new JwtToken();
    }
}   
