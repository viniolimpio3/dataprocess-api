using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace data_process_api.Controllers {
    public class PrecoPadraoController : GenericController<PrecoPadrao> {
        public PrecoPadraoController(DatabaseContext context) : base (context) { }
    }
}
