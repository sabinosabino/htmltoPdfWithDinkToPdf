using Microsoft.AspNetCore.Mvc;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace testehtmltopdf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IConverter _converter;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConverter converter)
        {
            _logger = logger;
            _converter = converter;
        }

        [HttpGet(Name = "GetReport")]
        public IActionResult GetReport()
        {
            try
            {
                string path = "/home/sabino/Documentos/projetos/csharp/testehtmltopdf/index.html";

                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings() { Top = 10 },
                        Out = @"test.pdf",
                    },
                    Objects = {
                        new ObjectSettings()
                        {
                            Page = path,
                        },
                    }
                };
                _converter.Convert(doc);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao gerar PDF: {ex.Message}");
            }
        }
    }
}
