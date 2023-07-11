using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        public HomeController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        // POST api/<HomeController>
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            string apiUrl = "https://gateway.vnpt-ca.vn/signservice/v4/oauth/authorize";
            //string baseAddress = "https://localhost:7275";
            var parameters = new Dictionary<string , string>
            {
                { "response_type" , "code" },
                { "client_id" , _appSettings.ClientId },
                { "state" , "" },
                { "redirect_uri" , apiUrl },
            };
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(apiUrl, new FormUrlEncodedContent(parameters));
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response API Gateway: " + responseBody);
                return Ok(responseBody);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
                return BadRequest();
            }
        } 
    }
}
