namespace data_process_api.Models {

    public class TipoEntrada {

        public int Id { get; set; }
        public string Descricao { get; set; }

        public TipoEntrada(string descricao)
        {
                this.Descricao = descricao;
        }

    }

}