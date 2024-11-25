namespace data_process_api.Models { 

    public class Frete { 
        public int Id { get; set; }
        public int IdRegiao { get; set; }
        public int IdEmpresaCliente { get; set; }
        public int IdCarro { get; set; }
        public int IdRepasseMotorista { get; set;}
        public double Valor { get; set;}
        public string Descricao { get; set;}
        public double Peso { get; set; }
        public string Endereco { get; set; }

    }
}