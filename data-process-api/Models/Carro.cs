using System.Text.RegularExpressions;

namespace data_process_api.Models {
    public class Carro {
        public int Id { get; set; }
        public string Modelo { get; set; }
        private string _ano { get; set; }
        public string Ano {
            get => _ano;
            set {
                if (value.Length != 4) {
                    throw new ArgumentException("Ano deve ter 4 dígitos");
                } else {
                    _ano = value;
                }
            }
        }
        public string Cor { get; set; }
        private string _placa { get; set; }
        public string Placa {
            get => this._placa;
            set {
                if (!ValidarPlaca(value))
                    throw new ArgumentException("Placa inválida. Use o formato 'XXX-9999' ou 'XXX9X99'.");

                this._placa = value;
            }
        }

        public string Renavam { get; set; }

        public static bool ValidarPlaca(string placa) {

            if (string.IsNullOrWhiteSpace(placa)) { return false; }

            if (placa.Length > 8) { return false; }

            placa = placa.Replace("-", "").Trim();
            if (char.IsLetter(placa, 4)) {
                // Verifica se a placa está no formato: três letras, um número, uma letra e dois números.
                var padraoMercosul = new Regex("[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}");
                return padraoMercosul.IsMatch(placa);
            } else {
                // Verifica se os 3 primeiros caracteres são letras e se os 4 últimos são números.
                var padraoNormal = new Regex("[a-zA-Z]{3}[0-9]{4}");
                return padraoNormal.IsMatch(placa);
            }
        }

    }
}