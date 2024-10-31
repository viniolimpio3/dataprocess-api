using data_process_api.Models;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace data_process_api.Tests.Unit {
    public class FuncionarioUnitTests {
        [Fact (DisplayName = "Nascimento deve ser data inválida")]
        public void Funcionario_NascimentoDeveSerDataValida() {

            // Arrange
            var funcionario = new Funcionario();
            var dataInvalida = DateTime.Now.AddYears(1); // Data futura

            // Act & Assert
            Assert.Throws<ArgumentException>(() => funcionario.Nascimento = dataInvalida);
        }

        [Fact (DisplayName = "Nome e celular Não podem estar vazios")]
        public void Funcionario_NomeNaoDeveSerNuloOuVazio() {

            // Arrange
            var funcionario = new Funcionario();

            // Act
            funcionario.Nome = "Vinicius";
            funcionario.Celular = "11944445555";

            // Assert
            Assert.NotEqual("", funcionario.Nome);
            Assert.NotEqual("", funcionario.Celular);
        }
    }
}
