using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace data_process_api.Controllers {
    public class RegiaoController : GenericController<Regiao> {
        public RegiaoController(DatabaseContext context) : base (context) { }
    }
}
