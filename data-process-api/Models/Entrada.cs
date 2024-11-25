namespace data_process_api.Models {

    public class Entrada {

        public int Id { get; set; }
        public string Descricao { get; set; }
        public int IdTipo { get; set; }
        public int IdFormaPagamento { get; set; }
        public int? IdEmpresaCliente { get; set; }
        public int? IdFrete { get; set; }
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataLimiteRecebimento { get; set;}
        public DateTime? DataRecebimento { get; set; }

    }
}