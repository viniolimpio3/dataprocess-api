using Newtonsoft.Json;

namespace data_process_api.Models {
    public class Funcionario {
        public int Id { get; set; }
        public string Nome { get; set; }

        private DateTime _nasc {  get; set; }

        public DateTime Nascimento {
            get => this._nasc;
            set {
                if (value > DateTime.Now.AddDays(1))
                    throw new ArgumentException("Data Inválida");
                else
                    this._nasc = value;
            }
        }
        public string? Celular { get; set; }
        public string? Endereco { get; set; }

        public int IdTipoFuncionario { get; set; }

    }
}
