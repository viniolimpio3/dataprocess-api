using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace data_process_api.Controllers {

    public class CarroFuncionarioController : GenericController<CarroFuncionario> {
        public CarroFuncionarioController(DatabaseContext context) : base(context) { }
    }
}
