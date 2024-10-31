using data_process_api.Models;

namespace data_process_api.Tests.Unit {
    public class FreteUnitTests {
        [Fact(DisplayName = "Valida se o valor do frete é calculado corretamente")]
        public void Frete_DeveCalcularValorTotalCorretamente() {
            // Arrange
            var frete = new Frete {
                Valor = 200,
                RepasseMotorista = 50
            };

            // Act
            var valorTotal = frete.Valor + frete.RepasseMotorista;

            // Assert
            Assert.Equal(250, valorTotal);
        }

        [Fact]
        public void Frete_DevePermitirAlterarPropriedades() {
            // Arrange
            var frete = new Frete {
                Id = 1,
                RegiaoId = 101,
                EmpresaClienteId = 202,
                CarroId = 303,
                RepasseMotorista = 50,
                Valor = 150.75,
                Descricao = "Carga de Equipamentos",
                Peso = 500.5,
                Endereco = "Rua Teste, 123"
            };

            // Act
            frete.Peso = 150.5;

            // Assert
            Assert.Equal(1, frete.Id);
            Assert.Equal(101, frete.RegiaoId);
            Assert.Equal(202, frete.EmpresaClienteId);
            Assert.Equal(303, frete.CarroId);
            Assert.Equal(50, frete.RepasseMotorista);
            Assert.Equal(150.75, frete.Valor);
            Assert.Equal("Carga de Equipamentos", frete.Descricao);
            Assert.Equal(150.5, frete.Peso);
            Assert.Equal("Rua Teste, 123", frete.Endereco);
        }
    }
}