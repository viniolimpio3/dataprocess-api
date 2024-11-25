using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace data_process_api.Controllers {
    public class EntradaController : GenericController<Entrada> {
        public EntradaController(DatabaseContext context) : base(context) { }

        public override async Task<ActionResult<IEnumerable<Entrada>>> GetAll() {
            try {
                var query = await _context.Entradas
                    .GroupJoin(
                        _context.FormasPagamento,
                        e => e.IdFormaPagamento,
                        fp => fp.Id,
                        (e, fp) => new { e, fp }
                    )
                    .SelectMany(
                        x => x.fp.DefaultIfEmpty(),
                        (x, fp) => new { x.e, fp }
                    )
                    .GroupJoin(
                        _context.TiposEntrada,
                        efp => efp.e.IdTipo,
                        tip => tip.Id,
                        (efp, tip) => new { efp, tip }
                    )
                    .SelectMany(
                        x => x.tip.DefaultIfEmpty(),
                        (x, tip) => new { x.efp.e, x.efp.fp, Tip = tip }
                    )
                    .GroupJoin(
                        _context.EmpresasCliente,
                        eft => eft.e.IdEmpresaCliente,
                        ec => ec.Id,
                        (eft, ec) => new { eft, ec }
                    )
                    .SelectMany(
                        x => x.ec.DefaultIfEmpty(),
                        (x, ec) => new { x.eft.e, x.eft.fp, x.eft.Tip, EmpresaCliente = ec }
                    )
                    .GroupJoin(
                        _context.Fretes,
                        eftec => eftec.e.IdFrete,
                        f => f.Id,
                        (eftec, f) => new { eftec, Frete = f }
                    )
                    .SelectMany(
                        x => x.Frete.DefaultIfEmpty(),
                        (x, f) => new {
                            Entrada = x.eftec.e,
                            FormaPagamento = x.eftec.fp,
                            TipoEntrada = x.eftec.Tip,
                            x.eftec.EmpresaCliente,
                            Frete = f
                        }
                    )
                    .ToListAsync();

                var result = query.Select(x => new
                {
                    Descricao = x.Entrada.Descricao,
                    DataEmissao = x.Entrada.DataEmissao,
                    DataRecebimento = x.Entrada.DataRecebimento,
                    DataLimiteRecebimento = x.Entrada.DataLimiteRecebimento,
                    FormaPagamento = x.FormaPagamento?.Tipo ?? "",
                    DadosPagamento = x.FormaPagamento?.DadosDePagamento ?? "",
                    Parcelas = x.FormaPagamento?.Parcelas,
                    Cliente = x.EmpresaCliente?.Nome ?? "Cliente não informado",
                    Email = x.EmpresaCliente?.Email ?? "Email não informado",
                    FreteDescricao = x.Frete?.Descricao ?? "Sem descrição",
                    FreteValor = x.Frete?.Valor ?? 0.0 
                }).ToList();


                return Ok(new ResponseModel { Success = true, Message = "Sucesso", Data = result });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message });
            }
        }

        [HttpGet("dash")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Entrada>>> Dash() {
            try {
                double entradas = await _context.Entradas.Join(
                    _context.Fretes,
                    e => e.IdFrete,
                    f => f.Id,
                    (e, f) => new { e, f }
                ).SumAsync(x => x.f.Valor);

                double saidas = await _context.Saidas.SumAsync(x => x.Valor);

                return Ok(new ResponseModel {
                    Success = true, Message = "Sucesso",
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
