using data_process_api.Models;
using data_process_api.Models.Context;
using data_process_api.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace data_process_api.Controllers {
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase {
        private readonly Models.Context.DatabaseContext _context;
        private readonly IConfiguration _configuration;


        public AuthController(Models.Context.DatabaseContext context, IConfiguration configuration) {
            this._configuration = configuration;
            this._context = context;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] Usuario model) {

            try {
                var userExists = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (userExists is not null)
                    return StatusCode(
                        StatusCodes.Status400BadRequest,
                        new ResponseModel { Success = false, Message = "Usuário já existe!" }
                    );

                await _context.Usuarios.AddAsync(new Usuario(model.Nome, model.Email, Crypt.HashPassword(model.Password)));
                await _context.SaveChangesAsync();

                return StatusCode(
                    StatusCodes.Status201Created,
                    new ResponseModel { Message = "Usuário criado com sucesso!" }
                );
            } catch (Exception ex) {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseModel { Success = false, Message = "Erro ao criar usuário. " + ex.Message }
                );
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] Usuario model) {

            try {
                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == Crypt.HashPassword(model.Password));

                if(user == null) {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseModel { Success = false, Message = "Usuário ou senha incorretos" });
                }
            

                var authClaims = new List<Claim> { 
                    new (ClaimTypes.Name, user.Email),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                return Ok(new ResponseModel { Success = true, Data = GetToken(authClaims)});

            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseModel { Message = "Erro ao realizar login: " + ex.Message, Success = false });
            }
        }

        private TokenModel GetToken(List<Claim> authClaims) {
            var authSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? _configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JWT_ISSUER") ?? _configuration["JWT:ValidIssuer"],
                audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new() {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }

        [HttpDelete]
        [Authorize]
        [Route("delete/{email}")]
        public async Task<IActionResult> DeleteAsync(string email) {

            try {
                var entity = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
                if (entity == null) {
                    return NotFound(new ResponseModel { Success = false, Message = "Not Found" });
                }

                _context.Usuarios.Remove(entity);
                await _context.SaveChangesAsync();

                return Ok(new ResponseModel { Success = true, Message = "Deletado com sucesso." });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao deletar. " + ex.Message });
            }
        }
    }
}
