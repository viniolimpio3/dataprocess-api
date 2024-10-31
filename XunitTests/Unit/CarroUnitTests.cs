using data_process_api.Models;

namespace data_process_api.Tests.Unit {
    public class CarroUnitTests {
        [Fact (DisplayName = "Deve atribuir valores �s propriedades de Carro")]
        public void Carro_DevePermitirAtribuirValoresAsPropriedades() {
            // Arrange
            var carro = new Carro {
                Id = 1,
                Modelo = "Fusca",
                Ano = "1985",
                Cor = "Azul",
                Placa = "ABC-1234",
                Renavam = "123456789"
            };

            // Assert
            Assert.Equal(1, carro.Id);
            Assert.Equal("Fusca", carro.Modelo);
            Assert.Equal("1985", carro.Ano);
            Assert.Equal("Azul", carro.Cor);
            Assert.Equal("ABC-1234", carro.Placa);
            Assert.Equal("123456789", carro.Renavam);
        }

        [Fact (DisplayName = "Ano deve ter quatro d�gitos")]
        public void Carro_AnoDeveTerQuatroDigitos() {
            // Arrange
            var carro = new Carro();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => carro.Ano = "85"); // Ano inv�lido
            Assert.Throws<ArgumentException>(() => carro.Ano = "198"); // Ano inv�lido
            carro.Ano = "1985"; // Ano v�lido

            Assert.Equal("1985", carro.Ano);
        }

        [Fact (DisplayName = "Placa deve ter formato v�lido")]
        public void Carro_PlacaDeveTerFormatoValido() {
            // Arrange
            var carro = new Carro();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => carro.Placa = "1234567"); // Placa inv�lida
            Assert.Throws<ArgumentException>(() => carro.Placa = "AB-1234"); // Placa inv�lida
            carro.Placa = "ABC-1234"; // Placa v�lida

            Assert.Equal("ABC-1234", carro.Placa);
        }

    }
}