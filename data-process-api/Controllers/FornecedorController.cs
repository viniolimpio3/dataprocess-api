using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace data_process_api.Controllers {
    public class FornecedorController : GenericController<Fornecedor> {
        public FornecedorController(DatabaseContext context) : base (context) { }
    }
}
