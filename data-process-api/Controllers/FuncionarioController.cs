using Asp.Versioning;
using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace data_process_api.Controllers {
    [ApiController]
    [Produces("application/json")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class FuncionarioController : ControllerBase {

        private readonly Models.Context.DatabaseContext _context;

        public FuncionarioController(Models.Context.DatabaseContext context) { 
            _context = context;
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string GetAnonymous() => "Anônimo";


        // GET: api/<FuncionarioController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios() {
            try {
                List<Funcionario> funcionarios = await _context.Funcionarios.ToListAsync();
                
                return Ok(new ResponseModel { Success = true, Message = "Sucesso", Data = funcionarios});
            } catch (Exception ex) {
                return StatusCode(
                    StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message, Data = ex.Data }
                );
            }
        }

        // GET api/<FuncionarioController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(string id) {
            try {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync( u => u.Id == int.Parse(id));

                if (funcionario == null) { 
                    return NotFound(new ResponseModel { Success = false, Message = "Not Found", Data = {} });
                }

                return Ok(new ResponseModel { Success = true, Message = "Found", Data = funcionario });

            } catch (Exception ex) {
                return StatusCode(
                    StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message }
                );
            }
        }

        // POST api/<FuncionarioController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Funcionario req) {
            try {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(u => u.Id == req.Id);

                if (funcionario != null) {
                    return Conflict(new ResponseModel { Success = false, Message = "Registro já existe" });
                }

                await _context.Funcionarios.AddAsync(req);
                await _context.SaveChangesAsync();

                return Ok(new ResponseModel { Success = true, Message = "Inserido com sucesso.", Data = req });

            } catch (Exception ex) {
                return StatusCode(
                    StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao inserir. " + ex.Message }
                );
            }
        }

        // PUT api/<FuncionarioController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(string id, [FromBody] Funcionario req) {
            try {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(u => u.Id == int.Parse(id));

                if (funcionario == null) {
                    return NotFound(new ResponseModel { Success = false, Message = "Registro já existe" });
                }

                funcionario = req;

                await _context.SaveChangesAsync();

                return Ok(new ResponseModel { Success = true, Message = "Inserido com sucesso.", Data = req });

            } catch (Exception ex) {
                return StatusCode(
                    StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao inserir. " + ex.Message }
                );
            }
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                var funcionario = await _context.Funcionarios.Where(u => u.Id == id).ExecuteDeleteAsync();

                await _context.SaveChangesAsync();

                return Ok(new ResponseModel { Success = true, Message = "Deletado com sucesso.", Data = Empty });

            } catch (Exception ex) {
                return StatusCode(
                    StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao deletar. " + ex.Message }
                );
            }
        }
    }
}
