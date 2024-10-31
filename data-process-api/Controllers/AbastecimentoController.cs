using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace data_process_api.Controllers {
    public class AbastecimentoController : GenericController<Abastecimento> {
        public AbastecimentoController(DatabaseContext contexto) : base(contexto) { }
    }
}
