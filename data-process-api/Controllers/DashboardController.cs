using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace data_process_api.Controllers {
    [ApiController]
    [Produces("application/json")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public abstract class DashboardController<TEntity> : ControllerBase {
        private readonly DatabaseContext _context;

        protected DashboardController(DatabaseContext context) {
            _context = context;
        }

        // GET: api/dashboard
        // Busca informações do dashboard inicial
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetAll() {
            try {
                var entities = await _context.Usuarios.CountAsync();


                return Ok(new ResponseModel { Success = true, Message = "Sucesso", Data = entities });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message });
            }
        }
    }
}
