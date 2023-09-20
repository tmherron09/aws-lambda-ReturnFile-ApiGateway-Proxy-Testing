using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace GetFileLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetFileController : ControllerBase
    {
        private FlurlClient _client;
        private readonly string _url;
        public GetFileController(IConfiguration config)
        {
            //_url = "https://twijlkd0m0.execute-api.us-east-1.amazonaws.com/test/get";
            _url = config["functionUrl"];
            _client = new FlurlClient(_url);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _url.WithHeader("Accept", "application/pdf")
                    .GetAsync();

                //var pathToFile = "D:\\aws learning\\PDF Passing Test\\HiTim.pdf";
                //var currentDir = Directory.GetCurrentDirectory();
                //var fileExists = System.IO.File.Exists(pathToFile);
               

                //return Ok();
                //var pdfBlob = System.IO.File.ReadAllBytes(pathToFile);

                //var encoded = Convert.ToBase64String(pdfBlob);

                var pdfBlob = Convert.FromBase64String(await response.GetStringAsync());

                //var response = await _url.WithHeader("Accept", "application/pdf")
                //.GetStreamAsync();

                //var pdfBlog = Convert.FromBase64String(response);
                //var pdfBlog = await response.GetBytesAsync();



                return File(pdfBlob, "application/pdf", "test.pdf");

                //using (MemoryStream ms = new MemoryStream())
                //{
                //    await response.CopyToAsync(ms);
                //    var pdfBlob = ms.ToArray();
                //    return File(pdfBlob, "application/pdf");
                //}
            }
            catch (FlurlHttpException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Error Getting File.");
            }
            //.WithHeader("Content-Type", "application/json")
        }

    }
}
