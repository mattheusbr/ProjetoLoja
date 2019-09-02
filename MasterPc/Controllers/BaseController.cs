using MasterPc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterPc.Controllers
{
    //Controllers herda ela sempre
    public class BaseController : Controller
    {
        protected HomeContext _homeContext = new HomeContext();

        //Evento que executa antes de redenrizar a view
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            ViewBag.Categorias = new MenuCategoria()
            {
                Categorias = _homeContext.CategoriaProdutoes.Select(x => new Categoria() { Id = x.Id, Nome = x.Nome }).ToList()
            };
        }        
    }
}