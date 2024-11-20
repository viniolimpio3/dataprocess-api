namespace data_process_api.Models{
    public class Fornecedor {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string TipoFornecedor { get; set; }
        public string Endereco { get; set; }
        public string Telefones { get; set; }
        public bool? Status { get; set; }
        public string? Email { get; set; }
        public int? IdFormaPagamento { get; set; }

        //public Fornecedor(string nome, string tipoFornecedor, string endereco,string telefone, bool status,string email, int formaPagamento)
        //{
        //    this.Nome = nome;
        //    this.TipoFornecedor = tipoFornecedor;
        //    this.Endereco = endereco;
        //    this.Telefones = telefone;
        //    this.Status = status;
        //    this.Email = email;
        //    this.FormaPagamentoId = formaPagamento;

        //}
    }
}