using MasterPc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MasterPc.Controllers
{
    public class HomeController : Controller
    {
        HomeContext db = new HomeContext();

        //===Mostrar os produtos no catalogo===

        //--Catalogo Inicial
        public ActionResult Index()
        {
            var produtoes = db.Produtoes.Select(x => x).ToList();

            return View(produtoes);
        }

        //===Categorias===
        public ActionResult Categoria(int id)
        {
            var categorias = db.Produtoes.Select(x => x).Where(x => x.CategoriaId == id).ToList();

            return View(categorias);
        }


        //Mostrar Produto detalhado para comprar 
        public ActionResult Produto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

    }
}