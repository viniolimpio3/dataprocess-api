namespace data_process_api.Tests.Integration {

    using data_process_api.Controllers;
    using data_process_api.Models;
    using data_process_api.Models.Context;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Xunit;
    using Xunit.Extensions.Ordering;

    [TestCaseOrderer( ordererTypeName: "XUnit.Project.Orderers.AlphabeticalOrderer", ordererAssemblyName: "XUnit.Project")]
    public class TokenIntegrationTests {

        AuthController _auth;

        public TokenIntegrationTests() {

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseMySql("Server=localhost;Database=data_process_db;User=root;Password=root;", new MySqlServerVersion(new Version(8, 0, 39)))
            .Options;

            DatabaseContext _context = new DatabaseContext(options);

            var myConfiguration = new Dictionary<string, string> {
                { "JWT:ValidAudience", "http://localhost:4200" },
                { "JWT:ValidIssuer", "http://localhost:5000" },
                { "JWT:Secret", "ADLFKJPOK#$#_$@#I(ASFÇAFAFLDSJF__@$@#%JDFLKSJDFV>ZXZ3" },
                { "DefaultConnection", "Server=localhost;Database=data_process_db;User=root;Password=root;" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            // Inicializa o controller
            _auth = new AuthController(_context, configuration);
        }

        private async Task<TokenModel> ObterTokenDeAutenticacaoAsync() {

            var response = await _auth.LoginAsync(new UserLoginModel { Email = "teste@teste.com", Password = "1234" });

            var okResult = response as ObjectResult;

            Assert.IsType<OkObjectResult>(okResult);

            var resModel = (okResult.Value as ResponseModel);

            Assert.IsType<ResponseModel>(resModel);

            var valueResult = resModel.Data as TokenModel;

            Assert.IsType<TokenModel>(valueResult);

            return valueResult;
        }


        [Fact(DisplayName = "0 - BlackBox - Register user in JWT")]
        public async Task TestCreate() {

            var response = await _auth.CreateUserAsync(new Usuario { Email = "teste@teste.com", Password = "1234", Nome = "Vini" });

            var okResult = response as ObjectResult;

            Assert.IsType<ObjectResult>(okResult);

            var resModel = (okResult.Value as ResponseModel);

            Assert.IsType<ResponseModel>(resModel);

            Assert.True(resModel.Success);
        }

        [Fact(DisplayName = "1 - BlackBox - Login JWT")]
        public async Task TestLogin() {

            var token = await ObterTokenDeAutenticacaoAsync();

            Assert.IsType<TokenModel>(token);

            Assert.NotEmpty(token.Token);
        }

        [Fact(DisplayName = "2 - BlackBox - Deletar usuário")]
        public async Task TestDelete() {

            var response = await _auth.DeleteAsync("teste@teste.com");

            var okResult = response as ObjectResult;

            Assert.IsType<OkObjectResult>(okResult);

            var resModel = (okResult.Value as ResponseModel);

            Assert.IsType<ResponseModel>(resModel);

            Assert.True(resModel.Success);
        }
    }

}
