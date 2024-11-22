using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace data_process_api.Controllers {
    public class CarroController : GenericController<Carro> {
        public CarroController(Models.Context.DatabaseContext context) : base(context) { }

        // GET: api/Carro/Dash
        [HttpGet("Dash")]
        [Authorize]
        public async Task<ActionResult> DashInfos() {
            try {
                double gastoAbastecimento = await _context.Abastecimentos
                    .SumAsync(a => a.Valor);

                double gastosAdicionais = await _context.Saidas 
                    .Where(s => (s.IdCarro != null || s.IdManutencao != null))
                    .SumAsync(s => s.Valor);

                int qtdVeiculos = await _context.Carros.CountAsync();

                var valorAPagar = await _context.Saidas
                    .Where(s => (s.IdCarro != null || s.IdManutencao != null) && s.DataPagamento == null)
                    .SumAsync(s => s.Valor);

                return Ok(new ResponseModel {
                    Success = true,
                    Message = "Sucesso",
                    Data = new {
                        qtdVeiculos,
                        gastosAdicionais,
                        gastoAbastecimento,
                        GastosTotais = gastosAdicionais + gastoAbastecimento,
                        valorAPagar
                    }
                });

            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message });
            }
        }

    }
}
