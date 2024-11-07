namespace data_process_api.Models {
    
    public class Regiao {

        public int Id { get; set; }
        public string RegiaoEntrega { get; set; }
        public String Cidade { get; set; }
        public string Bairro { get; set; }
        public double DistanciaEmKm { get; set; }

        public Regiao( string regiaoEntrega,string cidade, string bairro, double distanciaEmKm)
        {
            this.RegiaoEntrega = regiaoEntrega;
            this.Cidade = cidade;
            this.Bairro = bairro;
            this.DistanciaEmKm = distanciaEmKm;
            
        }
    }
}