namespace data_process_api.Tests.Performance {

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
    [TestCaseOrderer(
    ordererTypeName: "XUnit.Project.Orderers.AlphabeticalOrderer",
    ordererAssemblyName: "XUnit.Project")]

    public class TokenIntegrationTests {

        FuncionarioController _controller;
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
            _controller = new FuncionarioController(_context);
            _auth = new AuthController(_context, configuration);
        }

        private async Task<TokenModel> ObterTokenDeAutenticacaoAsync() {
            
            var response = await _auth.LoginAsync(new Usuario { Email = "vini@teste.com", Password = "1234" });

            var okResult = response as ObjectResult;

            Assert.IsType<OkObjectResult>(okResult);

            var valueResult = (okResult.Value as ResponseModel).Data as TokenModel;

            Assert.IsType<TokenModel>(valueResult);


            return valueResult;
        }

        [Fact(DisplayName = "0 - Performance - POST Funcionario em até 2 segundos")]
        public async Task Teste_0_PerformancePostFuncionario() {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var token = await ObterTokenDeAutenticacaoAsync();



            var response = await _controller.Create(
                new Funcionario {
                    Id = 100,
                    Nome = "José",
                    Nascimento = DateTime.Now.AddYears(-30),
                    Endereco = "Rua Limão 125",
                    Celular = "11944445555",
                    IdTipoFuncionario = 1
                }
            );

            stopwatch.Stop();

            var result = response as ObjectResult;

            var val = result.Value as ResponseModel;

            Assert.IsType<ResponseModel>(val);

            Assert.True(val.Success);

            Assert.InRange(stopwatch.ElapsedMilliseconds, 0, 2000);
        }

        [Fact(DisplayName = "1 - Performance - GET /id Funcionario em até 2 segundos"), Order(2)]
        public async Task Teste_1_PerformanceGetFuncionario() {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var token = await ObterTokenDeAutenticacaoAsync();

            var response = await _controller.GetById(100);

            stopwatch.Stop();

            var result = response as ObjectResult;

            Assert.IsType<OkObjectResult>(result);

            var val = result.Value as ResponseModel;

            Assert.IsType<ResponseModel>(val);

            Assert.True(val.Success);

            Assert.InRange(stopwatch.ElapsedMilliseconds, 0, 2000);
        }

        [Fact(DisplayName = "2 - Performance - GET Funcionarios em até 3 segundos"), Order(3)]
        public async Task Teste_2_PerformanceGetFuncionarios() {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var token = await ObterTokenDeAutenticacaoAsync();

            var response = await _controller.GetAll();

            stopwatch.Stop();

            var result = response.Result as ObjectResult;

            var val = result.Value as ResponseModel;

            Assert.IsType<ResponseModel>(val);

            Assert.True(val.Success);

            Assert.InRange(stopwatch.ElapsedMilliseconds, 0, 3000);
        }

        [Fact(DisplayName = "3 - Performance - PUT Funcionario em até 2 segundos"), Order(4)]
        public async Task Teste_3_PerformancePutFuncionario() {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var token = await ObterTokenDeAutenticacaoAsync();

            var response = await _controller.Update(
                100,
                new Funcionario {
                    Id = 100,
                    Nome = "Outro nome",
                    Nascimento = DateTime.Now.AddYears(-30),
                    Endereco = "Rua Limão 125",
                    Celular = "11944445555",
                    IdTipoFuncionario = 1
                }
            );

            stopwatch.Stop();

            var result = response as ObjectResult;

            Assert.IsType<OkObjectResult>(result);

            var val = result.Value as ResponseModel;

            Assert.IsType<ResponseModel>(val);

            Assert.True(val.Success);
            Assert.InRange(stopwatch.ElapsedMilliseconds, 0, 2000);
        }

        [Fact(DisplayName = "4 - Performance - DELETE Funcionario em até 2 segundos"), Order(5)]
        public async Task Teste_4_PerformanceDeleteFuncionario() {
            var stopwatch = new Stopwatch();
            

            stopwatch.Start();
            var token = await ObterTokenDeAutenticacaoAsync();
            var response = await _controller.Delete(100);
            stopwatch.Stop();

            response = response as ObjectResult;

            Assert.IsType<OkObjectResult>(response);
            Assert.InRange(stopwatch.ElapsedMilliseconds, 0, 2000);
        }
    }

}
