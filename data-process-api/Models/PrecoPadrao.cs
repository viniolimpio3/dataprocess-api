namespace data_process_api.Models {
    public class PrecoPadrao() {
        public int Id { get; set; }
        public double Valor { get; set; }
        public int EmpresaClienteId { get; set; }
        public int RegiaoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }


        public PrecoPadrao(double valor, int empresaClienteId, int regiaoId, DateTime dataCadastro, DateTime dataAlteracao)
        {
            this.Valor = valor;
            this.EmpresaClienteId = empresaClienteId;
            this.RegiaoId = regiaoId;
            this.DataCadastro = dataCadastro;
            this.DataAlteracao = dataAlteracao;


        }
    }


    
}