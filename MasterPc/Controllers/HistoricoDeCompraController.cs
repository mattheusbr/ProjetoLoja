using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterPc.Filtros;
using MasterPc.Models;

namespace MasterPc.Controllers
{
    [AutorizacaoFilter]
    public class HistoricoDeCompraController : BaseController
    {
        public ActionResult Listar()
        {
            Usuario usuario = this.PegarUsuario();

            var historico = _homeContext.Compras.Where(x => x.Status == Types.StatusCompra.Fechado && x.UsuarioId == usuario.Id).ToList();
            return View(historico);
        }
    }
}