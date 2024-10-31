using data_process_api.Models;
using data_process_api.Models.Context;

namespace data_process_api.Controllers {
    public class FuncionarioController : GenericController<Funcionario> {
        public FuncionarioController(DatabaseContext context) : base(context) { }
        
    }
}
