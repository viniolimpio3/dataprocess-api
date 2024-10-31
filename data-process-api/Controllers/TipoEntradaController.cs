﻿using data_process_api.Models;
using data_process_api.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace data_process_api.Controllers {
    public class TipoEntradaController : GenericController<TipoEntrada> {
        public TipoEntradaController(DatabaseContext context) : base(context) { }
    }
}
