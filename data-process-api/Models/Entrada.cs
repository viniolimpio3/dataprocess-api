namespace data_process_api.Models { 

    public class Entrada {

          public int Id { get; set; }
          public string Descricao { get; set; }
          public int TipoId { get; set; }
          public int FormaPagamentoId { get; set; }
          public int EmpresaClienteId { get; set; }
          public int FreteId { get; set; }
          public DateTime DataEmissao { get; set; }
          public DateTime DataLimiteRecebimento { get; set; }
          public DateTime DataRecebimento { get; set; }


    }
}