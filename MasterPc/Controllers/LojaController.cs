using MasterPc.Filtros;
using MasterPc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MasterPc.Controllers
{
    public class LojaController : Controller
    {
        private HomeContext contexto = new HomeContext();

        public ActionResult Catalogo()
        {
            var produtoes = contexto.Produtoes.Select(x => x).ToList();

            return View(produtoes);
        }

        //===Categorias===
        [Route ("Produtos/Categoria/{id}")]
        public ActionResult ListarCategoria(int id)
        {
            var categorias = contexto.Produtoes.Select(x => x).Where(x => x.CategoriaId == id).ToList();

            return View(categorias);
        }

        //Mostrar Produto detalhado para comprar 
        public ActionResult ProdutoDetalhar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = contexto.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }
        [AutorizacaoFilter]
        [Route ("Compra/Sucesso")]
        public ActionResult Compra()
        {
            return View();
        }

    }
}