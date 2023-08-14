using BemolChallenge.Data;
using BemolChallenge.Models;
using BemolChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace BemolChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BemolController : ControllerBase
    {
        private readonly BemolContex _context;
        private readonly IAzureBusService _busService;
        public BemolController(BemolContex context, IAzureBusService busService  ) 
        { 
            _context = context;            
            _busService = busService;
        }        

        [HttpPost(Name = "Bemol")]
        public async Task <IActionResult> Get([FromBody]BemolObject bemol)
        {
            //Validando objeto recebido
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            try
            {
                //Salvando no banco de dados
                await _context.AddAsync(bemol);
                await _context.SaveChangesAsync();
                //Enviando para o ServiceBus
                await _busService.SendMessageAsync(bemol, "queueName");
                return Ok(bemol);   
            }
            catch(Exception) {
                return StatusCode(500);
            }
        }
        
    }
}