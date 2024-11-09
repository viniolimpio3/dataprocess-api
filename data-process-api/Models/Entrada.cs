namespace data_process_api.Models { 

    public class Entrada {

        public int Id { get; set; }
        public string Descricao { get; set; }
        public int TipoId { get; set; }
        public int FormaPagamentoId { get; set; }
        public int EmpresaClienteId { get; set; }
        public int FreteId { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataLimiteRecebimento { get; set; }
        public DateTime DataRecebimento { get; set; }

        public Entrada(string descricao,int tipoId,int formaPagamentoId,int empresaClienteId,int freteId,DateTime dataEmissao, DateTime dataLimiteRecebimento,DateTime dataRecebimento)
        {
            this.Descricao = descricao;
            this.TipoId = tipoId;
            this.FormaPagamentoId = formaPagamentoId;
            this.EmpresaClienteId = empresaClienteId;
            this.FreteId = freteId;
            this.DataEmissao = dataEmissao;
            this.DataLimiteRecebimento = dataLimiteRecebimento;
            this.DataRecebimento = dataRecebimento;

            
        }




    }
}