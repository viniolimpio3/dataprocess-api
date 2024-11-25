using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace data_process_api.Controllers {
    public class FuncionarioController : GenericController<Funcionario> {
        public FuncionarioController(DatabaseContext context) : base(context) { }

        // GET: api/Funcionario/
        [HttpGet]
        [Authorize]
        public override async Task<ActionResult<IEnumerable<Funcionario>>> GetAll() {
            try {
                var entities = await _context.Funcionarios
                .GroupJoin(
                    _context.FormasPagamento,
                    funcionario => funcionario.IdFormaPagamento,
                    fp => fp.Id,
                    (funcionario, formasPagamento) => new { funcionario, formasPagamento }
                )
                .SelectMany(
                    x => x.formasPagamento.DefaultIfEmpty(),
                    (x, fp) => new { x.funcionario, fp }
                )
                .GroupJoin(
                    _context.TiposFuncionario,
                    ffp => ffp.funcionario.IdTipoFuncionario,
                    tipoFunc => tipoFunc.Id,
                    (ffp, tiposFuncionario) => new { ffp.funcionario, ffp.fp, tiposFuncionario }
                )
                .SelectMany(
                    x => x.tiposFuncionario.DefaultIfEmpty(),
                    (x, tipoFunc) => new {
                        x.funcionario.Id,
                        x.funcionario.Nome,
                        x.funcionario.Nascimento,
                        x.funcionario.Celular,
                        x.funcionario.Endereco,
                        TipoFuncionario = tipoFunc.Descricao ?? "Não informado", // Usando "Não informado" caso não haja tipo
                        FormaPagamento = x.fp.Tipo ?? "Não informado" // Usando "Não informado" caso não haja forma de pagamento
                    }
                )
                .ToListAsync();


                return Ok(new ResponseModel { Success = true, Message = "Sucesso", Data = entities });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message });
            }
        }

        // GET: api/Funcionario/
        [HttpGet("Folha")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Funcionario>>> FolhaPagamento() {
            try {
                var entities = await _context.RepassesMotorista.Join(
                    _context.Fretes,
                    repasse => repasse.Id,
                    frete => frete.IdRepasseMotorista,
                    (repasse, frete) => new { repasse, frete }
                )
                .Join(
                    _context.Funcionarios,
                    rf => rf.repasse.IdFuncionario,
                    funcionario => funcionario.Id,
                    (rf, funcionario) => new {
                        funcionario.Id,
                        funcionario.Nome,
                        rf.frete.Valor,
                        rf.repasse.Status,
                        rf.repasse.CustoAlimentacao,
                        rf.repasse.CustoHospedagem,
                        RepasseId = rf.repasse.Id,
                        FreteId = rf.frete.Id
                    }
                )
                .GroupBy(
                    x => new { x.Id, x.Nome },
                    (key, g) => new {
                        FuncionarioId = key.Id,
                        key.Nome,
                        ValorAPagar = g.Sum(x => x.Valor) + g.Sum(x => x.CustoHospedagem ?? 0) + g.Sum(x => x.CustoAlimentacao ?? 0),
                        TotalFretes = _context.RepassesMotorista.Where(r => r.IdFuncionario == key.Id).Count()
                    }
                ).ToListAsync();

                return Ok(new ResponseModel { Success = true, Message = "Sucesso", Data = entities });

            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message });
            }
        }

        // GET: api/Funcionario/
        [HttpGet("Dash")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Funcionario>>> DashInfos() {
            try {
                int countFuncionarios = await _context.Funcionarios.CountAsync();

                int countRepasses = await _context.RepassesMotorista.CountAsync();

                var valorAPagar = await _context.RepassesMotorista
                    .Where(r => r.Status == false)
                    .SumAsync(r => (r.CustoAlimentacao ?? 0) + (r.CustoHospedagem ?? 0) + r.Valor);

                return Ok(new ResponseModel {
                    Success = true,
                    Message = "Sucesso",
                    Data = new {
                        countFuncionarios,
                        countRepasses,
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
