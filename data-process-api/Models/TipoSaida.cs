namespace data_process_api.Models {
    public class TipoSaida {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public TipoSaida(string descricao)
        {
            this.Descricao = descricao;
            
        }
    }

}