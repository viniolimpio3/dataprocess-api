using Newtonsoft.Json;

namespace data_process_api.Models {
    public class Funcionario {
        public int Id { get; set; }
        public string Nome { get; set; }

        public DateTime Nascimento { get; set; }
        public string? Celular { get; set; }
        public string? Endereco { get; set; }


        public Funcionario(string nome, DateTime nascimento,string celular, string endereco)
        {
            this.Nome = nome; 
            this.Nascimento = nascimento;
            this.Celular = celular;
            this.Endereco = endereco;
        }
    }
}
