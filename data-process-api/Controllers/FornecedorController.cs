using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace data_process_api.Controllers {
    public class FornecedorController : GenericController<Fornecedor> {
        public FornecedorController(DatabaseContext context) : base (context) { }

        // GET: api/Fornecedor/
        [HttpGet]
        [Authorize]
        public override async Task<ActionResult<IEnumerable<Fornecedor>>> GetAll() {
            try {
                var entities = await _context.Fornecedores
                .GroupJoin(
                    _context.FormasPagamento,
                    forn => forn.IdFormaPagamento,
                    fp => fp.Id,
                    (forn, formasPagamento) => new { forn, formasPagamento }
                )
                .SelectMany(
                    x => x.formasPagamento.DefaultIfEmpty(),
                    (x, formaPag) => new {
                        x.forn.Id,
                        x.forn.Nome,
                        x.forn.Endereco,
                        x.forn.TipoFornecedor,
                        x.forn.Telefones,
                        x.forn.Email,
                        FormaPagamento = formaPag.Tipo ?? "Não informado",
                        DadosPagamento = formaPag.DadosDePagamento ?? "Não informado"
                    }
                )
                .ToListAsync();


                return Ok(new ResponseModel { Success = true, Message = "Sucesso", Data = entities });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message });
            }
        }

        // GET: api/Fornecedor/Dash
        [HttpGet("Dash")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Funcionario>>> DashInfos() {
            try {
                int countFornecedores = await _context.Fornecedores.CountAsync();

                double gastoFornecedores = await _context.Fornecedores
                .Join(
                    _context.Saidas,
                    fornecedor => fornecedor.Id,
                    saida => saida.IdFornecedor,
                    (fornecedor, saida) => new {
                        fornecedor, saida
                    }
                ).SumAsync(x => x.saida.Valor);

                double valorAPagar = await _context.Fornecedores
                .Join(
                    _context.Saidas,
                    fornecedor => fornecedor.Id,
                    saida => saida.IdFornecedor,
                    (fornecedor, saida) => new {
                        fornecedor, saida
                    }
                )
                .Where(x=> x.saida.DataPagamento == null)
                .SumAsync(x => x.saida.Valor);

                return Ok(new ResponseModel {
                    Success = true,
                    Message = "Sucesso",
                    Data = new {
                        countFornecedores,
                        gastoFornecedores,
                        valorAPagar
                    }
                });

            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message });
            }
        }

        // GET: api/Fornecedor/Pagamentos
        [HttpGet("Pagamentos")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Funcionario>>> Pagamentos() {
            try {
                var entities = await _context.Fornecedores
                .GroupJoin(
                    _context.FormasPagamento,
                    forn => forn.IdFormaPagamento,
                    fp => fp.Id,
                    (forn, formasPagamento) => new { forn, formasPagamento }
                )
                .SelectMany(
                    x => x.formasPagamento.DefaultIfEmpty(),
                    (x, fp) => new { x.forn, fp }
                )
                .GroupJoin(
                    _context.Saidas,
                    ffp => ffp.forn.Id,
                    saida => saida.IdFornecedor,
                    (ffp, saidas) => new { ffp.forn, ffp.fp, saidas}
                )
                .SelectMany(
                    x => x.saidas,
                    (x, saidas) => new {
                        x.forn.Nome,
                        x.forn.TipoFornecedor,
                        x.forn.Telefones,
                        x.forn.Email,
                        FormaPagamento = x.fp.Tipo ?? "Não informado",
                        DadosPagamento = x.fp.DadosDePagamento ?? "Não informado",
                        saidas.Valor
                    }
                )
                .ToListAsync();

                return Ok(new ResponseModel {
                    Success = true,
                    Message = "Sucesso",
                    Data = entities
                });

            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message });
            }
        }
    }
}
