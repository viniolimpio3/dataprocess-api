using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace data_process_api.Controllers {
    public class ManutencaoController : GenericController<Manutencao> {
        public ManutencaoController(DatabaseContext context) : base (context) { }

        // GET: api/manutencao/
        [HttpGet]
        [Authorize]
        public override async Task<ActionResult<IEnumerable<Manutencao>>> GetAll() {
            try {
                var entities = await _context.Manutencoes
                .Join(
                    _context.Carros,
                    man => man.IdCarro,
                    c => c.Id,
                    (man, car) => new { man, car}
                )
                .Join(
                    _context.FormasPagamento,
                    x => x.man.IdFormaPagamento,
                    fp => fp.Id,
                    (c, fp) => new { c, fp }
                    )
                .Select(
                    x => new {
                        x.c.man.Id,
                        x.c.man.Data,
                        x.c.man.TipoManutencao,
                        x.c.man.QuilometragemAtual,
                        x.c.man.Descricao,
                        x.c.man.ValorMaoDeObra,
                        x.c.man.ValorPecas,
                        x.c.man.DataPrevista,
                        x.c.man.QuilometragemPrevista,
                        x.c.car.Modelo, 
                        x.c.car.Placa,
                        FormaPagamento = x.fp.Tipo ?? "Não informado"
                    }
                )
                .ToListAsync();

                return Ok(new ResponseModel { Success = true, Message = "Sucesso", Data = entities });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message });
            }
        }
    }
}
