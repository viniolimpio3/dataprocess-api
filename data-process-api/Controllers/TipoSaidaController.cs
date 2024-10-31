using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace data_process_api.Controllers {

    public class TipoSaidaController : GenericController<TipoSaida> {
        public TipoSaidaController(DatabaseContext context) : base(context) { }
    }
}
