namespace data_process_api.Models { 
    public class Manutencao { 

          

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string TipoManutencao { get; set; }
        public int IdCarro { get; set; }
        public double QuilometragemAtual { get; set; }
        public string Descricao { get; set; }
        public double ValorMaoDeObra { get; set; }
        public double ValorPecas { get; set; }
        public int FormaPagamentoId { get; set; }
        public DateTime DataPrevista { get; set; }
        public double QuilometragePrevista { get; set; }

        //public Manutencao(DateTime date, string tipoManutencao,int carroId, int mecanicoId,double quilometragemAtual, string descricao,double valorMaoDeObra,double valorPecas,int formaPagamentoId,DateTime dataPrevista,double quilometragemPrevista)
        //{
        //    this.Date = date;
        //    this.TipoManutencao = tipoManutencao; 
        //    this.CarroId = carroId; 
        //    this.MecanicoId = mecanicoId;
        //    this.QuilometragemAtual = quilometragemAtual;
        //    this.Descricao = descricao;
        //    this.ValorMaoDeObra = valorMaoDeObra; 
        //    this.ValorMaoDeObra = ValorMaoDeObra; 
        //    this. ValorPecas = valorPecas;
        //    this.DataPrevista = dataPrevista;
        //    this.QuilometragePrevista = quilometragemPrevista;
                
        //}



    }
}