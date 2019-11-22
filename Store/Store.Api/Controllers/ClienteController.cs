using Store.Api.Models;
using Store.BusinessLogic.BL.Interfaces;
using Store.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Store.Api.Controllers
{
    [CarregarCliente]
    public class ClienteController : BaseController<IClienteBL>
    {
        public ClienteController(IClienteBL BLInjectable) : base(BLInjectable) { }

        [HttpPost]
        public Cliente Inserir([FromBody] Cliente cliente) {
            return this.BLInjected.Inserir(cliente);
        }

        [HttpPut]
        [Authorize]
        public Cliente Atualizar([FromBody] Cliente cliente) {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize]
        public Cliente Consultar()
        {
            throw new NotImplementedException();        
        }
    }
}
