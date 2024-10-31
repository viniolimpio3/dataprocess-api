using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace data_process_api.Controllers {
    public class EmpresaController : GenericController<EmpresaCliente> {
        public EmpresaController(DatabaseContext context) : base(context) { }
    }
}
