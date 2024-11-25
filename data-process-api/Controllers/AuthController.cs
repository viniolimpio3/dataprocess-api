using data_process_api.Models;
using data_process_api.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

                if (!Crypt.IsBase64String(model.Password) && !Crypt.IsBase64String(model.Email))
                    throw new Exception("Erro nos parâmetros enviados");

                model.Email = Encoding.UTF8.GetString(Convert.FromBase64String(model.Email));
                model.Password = Encoding.UTF8.GetString(Convert.FromBase64String(model.Password));

                if (userExists is not null)
                    return StatusCode(
                        StatusCodes.Status400BadRequest,
                        new ResponseModel { Success = false, Message = "Erro ao criar usuário!" }
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
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginModel model) {

            try {

                if (!Crypt.IsBase64String(model.Email) || !Crypt.IsBase64String(model.Password))
                    return Unauthorized(new ResponseModel { Success = false, Message = "Erro nos parâmetros enviados" });

                model.Email = Encoding.UTF8.GetString(Convert.FromBase64String(model.Email));
                model.Password = Encoding.UTF8.GetString(Convert.FromBase64String(model.Password));

                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == Crypt.HashPassword(model.Password));

                if (user == null) {
                    return Unauthorized(new ResponseModel { Success = false, Message = "Não autorizado" });
                }

                var authClaims = new List<Claim> {
                    new (ClaimTypes.Name, user.Email),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                return Ok(new ResponseModel { Success = true, Data = GetToken(authClaims) });

            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseModel { Message = "Erro ao realizar login: " + ex.Message, Success = false });
            }
        }

        private TokenModel GetToken(List<Claim> authClaims) {
            var authSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? _configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JWT_ISSUER") ?? _configuration["JWT:ValidIssuer"],
                audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(24),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new() {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }


        [HttpGet]
        [Authorize]
        [Route("me")]
        public async Task<IActionResult> GetCurrentUser() {

            try {
                var authorization = Request.Headers["Authorization"][0];

                if (string.IsNullOrEmpty(authorization))
                    return Unauthorized(new ResponseModel { Success = false, Message = "Não autorizado" });

                authorization = authorization.Replace("Bearer ", "");

                var token = new JwtSecurityToken(jwtEncodedString: authorization);
                var email = token.Claims.First(c => c.Type.Contains("identity/claims/name")).Value;

                if (string.IsNullOrEmpty(email))
                    return Unauthorized(new ResponseModel { Success = false, Message = "Não autorizado" });


                Usuario user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email.Equals(email));

                if (user == null)
                    return Unauthorized(new ResponseModel { Success = false, Message = "Não autorizado" });


                return Ok(new ResponseModel { Success = true, Data = user });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro ao deletar. " + ex.Message });
            }
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

        [HttpPost]
        [Authorize]
        [Route("recover")]
        public async Task<IActionResult> RecoverUser([FromBody] UserRecoverModel user) {

            try {

                // [TODO]
                return Ok(new ResponseModel { Success = true, Message = "[TODO]" });
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Success = false, Message = "Erro na recuperação. " + ex.Message });
            }
        }
    }
}
