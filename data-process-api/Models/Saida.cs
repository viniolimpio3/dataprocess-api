namespace data_process_api.Models {

    public class Saida {

        public int Id { get; set; }
        public int TipoId { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int FormaPagamentoIdId { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public int ManutencaoId { get; set; }
        public int Abastecimento { get; set; }
        public int CarroId { get; set; }
        public int FreteId { get; set; }
    }

}