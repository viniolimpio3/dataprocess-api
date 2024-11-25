namespace data_process_api.Models {

    public class FormaPagamento {
        public FormaPagamento() {
        }

        public int Id { get; set; } 
        public string? Tipo { get; set; }
        public string DadosDePagamento {  get; set; }
        public bool Status { get; set; }
        public string? Parcelas { get; set; }

    }
}
