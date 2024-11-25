using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace data_process_api.Controllers {
    [ApiController]
    [Produces("application/json")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class DashboardController<TEntity> : ControllerBase {
        private readonly DatabaseContext _context;

        protected DashboardController(DatabaseContext context) {
            _context = context;
        }

        // GET: api/dashboard
        // Busca informações do dashboard inicial
        [HttpGet("/financeiro")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetAll() {
            try {
                double entradas = await _context.Entradas.Join(
                    _context.Fretes,
                    e => e.IdFrete,
                    f => f.Id,
                    (e, f) => new { e, f }
                ).SumAsync( x => x.f.Valor);

                double saidas = await _context.Saidas.SumAsync(x => x.Valor);

                return Ok(new ResponseModel { Success = true, Message = "Sucesso", 
                    Data = new {
                        entradas,
                        saidas,
                        Lucro = entradas - saidas
                    }
                });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message });
            }
        }
    }
}
