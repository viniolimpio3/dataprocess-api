using data_process_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace data_process_api.Controllers {
    [ApiController]
    [Produces("application/json")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public abstract class GenericController<TEntity> : ControllerBase where TEntity : class {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected GenericController(DbContext context) {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        // GET: api/<EntityController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetAll() {
            try {
                var entities = await _dbSet.ToListAsync();
                return Ok(new ResponseModel { Success = true, Message = "Sucesso", Data = entities });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message });
            }
        }

        // GET api/<EntityController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id) {
            try {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null) {
                    return NotFound(new ResponseModel { Success = false, Message = "Not Found" });
                }

                return Ok(new ResponseModel { Success = true, Message = "Found", Data = entity });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao buscar. " + ex.Message });
            }
        }

        // POST api/<EntityController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] TEntity entity) {
            try {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();

                return StatusCode(
                    StatusCodes.Status201Created,
                    new ResponseModel { Success = true, Message = "Inserido com sucesso!", Data = entity }
                );
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao inserir. " + ex.Message });
            }
        }

        // PUT api/<EntityController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] TEntity updatedEntity) {
            try {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null) {
                    return NotFound(new ResponseModel { Success = false, Message = "Not Found" });
                }

                _context.Entry(entity).CurrentValues.SetValues(updatedEntity);
                await _context.SaveChangesAsync();

                return Ok(new ResponseModel { Success = true, Message = "Atualizado com sucesso.", Data = updatedEntity });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao atualizar. " + ex.Message });
            }
        }

        // DELETE api/<EntityController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id) {
            try {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null) {
                    return NotFound(new ResponseModel { Success = false, Message = "Not Found" });
                }

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();

                return Ok(new ResponseModel { Success = true, Message = "Deletado com sucesso." });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao deletar. " + ex.Message });
            }
        }
    }
}
