namespace data_process_api.Models { 
    public class Manutencao { 

          

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string TipoManutencao { get; set; }
        public int CarroId { get; set; }
        public int MecanicoId { get; set; }
        public double QuilometragemAtual { get; set; }
        public string Descricao { get; set; }
        public double ValorMaoDeObra { get; set; }
        public double ValorPecas { get; set; }
        public int FormaPagamentoId { get; set; }
        public DateTime DataPrevista { get; set; }
        public double QuilometragePrevista { get; set; }




    }
}