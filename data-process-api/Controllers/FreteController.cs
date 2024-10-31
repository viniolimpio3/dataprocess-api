using data_process_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace data_process_api.Controllers {
    public class FreteController : GenericController<Frete> {
        public FreteController(Models.Context.AppContext context) : base(context) { }
    }
}
