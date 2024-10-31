namespace data_process_api.Models {
    public class Carro {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        public string Cor { get; set; }
        public string Placa { get; set; }
        //public string Documento { get; set; }
        //public byte[] Documento { get; set; }
        public string Renavam { get; set; } 

        public ICollection<Funcionario>? Funcionarios { get; set; }
    }
}