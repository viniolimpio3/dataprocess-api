namespace data_process_api.Models {
    public class RepasseMotorista {
        public int Id { get; set; }
        public int IdFuncionario { get; set; }
        public double Valor { get; set; }
        public double? CustoHospedagem { get; set; }
        public double? CustoAlimentacao { get; set; }

        public bool? Status {  get; set; }
    }
}
