namespace data_process_api.Models{
    public class Fornecedor {

        public int ID { get; set; }
        public string Nome { get; set; }
        public string TipoFornecedor { get; set; }
        public string Endereco { get; set; }
        public string Telefones { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
        public int FormaPagamentoId { get; set; }
    }
}