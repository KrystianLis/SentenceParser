using System;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentenceController : ControllerBase
    {
        private readonly ISentenceParserService _service;

        public SentenceController(ISentenceParserService service)
        {
            _service = service;
        }

        [HttpGet("GetCsv/{id}")]
        public async Task<IActionResult> GetCsvFileAsync(int id)
        {
            try
            {
                return Ok(await _service.CreateCsvAsync(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet("GetXml/{id}")]
        public async Task<IActionResult> GetResultAsync(int id)
        {
            try
            {
                return Ok(await _service.CreateXmlAsync(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostSentenceAsync([FromBody] SentenceDto value)
        {
            if(value is null)
            {
                return BadRequest();
            }

            int id = await _service.AddSentenceAsync(value);
            return Ok(new { Id = id });
        }
    }
}
