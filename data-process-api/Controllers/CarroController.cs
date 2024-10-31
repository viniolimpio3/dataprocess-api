using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace data_process_api.Controllers {
    public class CarroController : GenericController<Carro> {
        public CarroController(Models.Context.AppContext context) : base(context) { }
    }
}
