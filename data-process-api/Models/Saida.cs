namespace data_process_api.Models {

    public class Saida {

        public int Id { get; set; }
        public int? IdTipo { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int? IdFormaPagamento { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public int? IdManutencao { get; set; }
        public int? IdCarro { get; set; }
        public int? IdFrete { get; set; }
        public int? IdFornecedor { get; set; }
    }

}