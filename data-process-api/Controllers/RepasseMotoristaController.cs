using data_process_api.Models.Context;
using data_process_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace data_process_api.Controllers {
    public class RepasseMotoristaController : GenericController<RepasseMotorista> {
        public RepasseMotoristaController(DatabaseContext context) : base(context) { }
    }
}
