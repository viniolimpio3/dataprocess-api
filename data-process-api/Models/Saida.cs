namespace data_process_api.Models {

    public class Saida {

        public int Id { get; set; }
        public int? TipoId { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int? IdFormaPagamento { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public int? IdManutencao { get; set; }
        public int? Abastecimento { get; set; }
        public int? IdCarro { get; set; }
        public int? IdFrete { get; set; }
        public int? IdFornecedor { get; set; }

        //public Saida(int tipoId, string descricao, double valor, int formaPagamentoId, DateTime dataEmissao, DateTime dataVencimento, DateTime dataPagamento,int manutencaoId,int abastecimento,int carroId,int freteId)
        //{
        //    this.TipoId = tipoId;
        //    this.Descricao = descricao;
        //    this.Valor = valor;
        //    this.FormaPagamentoIdId = formaPagamentoId;
        //    this.DataEmissao = dataEmissao;
        //    this.DataVencimento = dataVencimento;
        //    this.DataPagamento = dataPagamento;
        //    this.ManutencaoId = manutencaoId;
        //    this.Abastecimento = abastecimento;
        //    this.CarroId = carroId;
        //    this.FreteId = freteId;

        //}
    }

}