namespace data_process_api.Models {

    public class Abastecimento {

        public int Id { get; set; }
        public string? TipoCombustivel { get; set; }
        public double Valor { get; set; }
        public string? Cidade { get; set; }
        public Carro Carro { get; set; }
        public double QuilometragemAtual { get; set; }
        public DateTime Date { get; set; }

        public Abastecimento(string tipoCombustivel,double valor,string cidate, Carro carro, double quilometragemAtual,DateTime date)
        {
            this.TipoCombustivel = tipoCombustivel;
            this.Valor = valor;
            this.Cidade = cidate;
            this.Carro = carro;
            this.QuilometragemAtual = quilometragemAtual;
            this.Date = date;
        }

    }

}