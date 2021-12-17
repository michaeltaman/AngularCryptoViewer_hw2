using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CoinsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<JsonResult> GetAsync()
        {
       
            string url = _configuration.GetConnectionString("mainUrlAPI");

            JObject json = null;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                var response = client.GetAsync(url).Result;

                string content = response.Content.ReadAsStringAsync().Result;

                json = JObject.Parse(content);
                //Debug.WriteLine(json["data"].ToString());
            }

            return new JsonResult(json);

        }
    }
}
