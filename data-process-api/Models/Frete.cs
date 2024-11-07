namespace data_process_api.Models { 

    public class Frete { 
        public int Id { get; set; }
        public int RegiaoId { get; set; }
        public int EmpresaClienteId { get; set; }
        public int CarroId { get; set; }
        public int RepasseMotorista { get; set;}
        public double Valor { get; set;}
        public string Descricao { get; set;}
        public double Peso { get; set; }
        public string Endereco { get; set; }


        public Frete(int regiaoId,int empresaClienteId,int carroId,int repasseMotorista, double valor,string descricao,double peso,string endereco)
        {
            this.RegiaoId = regiaoId;
            this.EmpresaClienteId = empresaClienteId;
            this.CarroId =carroId;
            this.RepasseMotorista = repasseMotorista; 
            this.Valor = valor; 
            this.Descricao = descricao; 
            this.Peso = peso; 
            this.Endereco = endereco;
            
        }
    }
}