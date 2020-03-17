using MasterPc.DAO;
using MasterPc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterPc.Controllers
{
    public class BaseController : Controller
    {
        public HomeContext _homeContext = new HomeContext();
        public UsuariosDAO _usuariodao = new UsuariosDAO();

        //Executa antes de redenrizar a view
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            ViewBag.Categorias = new MenuCategoria()
            {
                Categorias = _homeContext.CategoriaProdutoes.Select(x => new Categoria() { Id = x.Id, Nome = x.Nome }).ToList()
            };
        }

        protected Usuario PegarUsuario()
        {
            Usuario usuario = null;

            if (Session["usuarioLogado"] != null)
            {
                usuario = Session["usuarioLogado"] as Usuario;
            }

            return usuario;
        }

    }
}