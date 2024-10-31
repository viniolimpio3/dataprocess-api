namespace data_process_api.Models {

    public class Abastecimento {

        public int Id { get; set; }
        public string? TipoCombustivel { get; set; }
        public double Valor { get; set; }
        public string? Cidade { get; set; }
        public Carro Carro { get; set; }
        public double QuilometragemAtual { get; set; }
        public DateTime Date { get; set; }

    }

}