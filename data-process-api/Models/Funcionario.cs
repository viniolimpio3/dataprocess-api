using data_process_api.Util;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace data_process_api.Models {
    public class Funcionario {
        public int Id { get; set; }
        public string Nome { get; set; }

        private DateTime _nasc {  get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime Nascimento {
            get => _nasc; 
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
        public int? IdFormaPagamento { get; set; }

    }
}
