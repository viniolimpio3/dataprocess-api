using data_process_api.Models;
using data_process_api.Models.Context;

namespace data_process_api.Controllers {
    public class FormaPagamentoController : GenericController<FormaPagamento> {
        public FormaPagamentoController(DatabaseContext context) : base(context) { }
    }
}
