namespace data_process_api.Models {

    public class TipoFuncionario {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public TipoFuncionario(string descricao)
        {
            this.Descricao = descricao;
        }
    }
}