using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace data_process_api.Controllers {

    public class SaidaController : GenericController<Saida> {
        public SaidaController(DatabaseContext context) : base(context) { }
    }
}
