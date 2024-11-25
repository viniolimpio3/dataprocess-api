using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace data_process_api.Controllers {
    public class AbastecimentoController : GenericController<Abastecimento> {
        public AbastecimentoController(DatabaseContext contexto) : base(contexto) { }
        // GET: api/manutencao/
        [HttpGet]
        [Authorize]
        public override async Task<ActionResult<IEnumerable<Abastecimento>>> GetAll() {
            try {
                var entities = await _context.Abastecimentos
                .Join(
                    _context.Carros,
                    ab => ab.IdCarro,
                    c => c.Id,
                    (ab, car) => new { ab, car }
                )
                .Select(
                    x => new {
                        x.ab.Id,
                        x.ab.TipoCombustivel,
                        x.ab.Valor,
                        x.ab.QuilometragemAtual,
                        x.ab.Cidade,
                        x.ab.Data,
                        x.car.Modelo,
                        x.car.Placa
                    }
                )
                .OrderByDescending(x => x.Data)
                .ToListAsync();

                return Ok(new ResponseModel { Success = true, Message = "Sucesso", Data = entities });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message });
            }
        }
    }
}
