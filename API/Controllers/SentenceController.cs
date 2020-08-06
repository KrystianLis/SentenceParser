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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResultAsync(int id)
        {
            try
            {
                return Ok(await _service.GetWordsAsync(id));
            }
            catch(Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SentenceDto value)
        {
            if(value is null)
            {
                return BadRequest();
            }

            int id = await _service.AddSentenceAsync(value);
            return CreatedAtAction(nameof(GetResultAsync), new { Id = id });
        }
    }
}
