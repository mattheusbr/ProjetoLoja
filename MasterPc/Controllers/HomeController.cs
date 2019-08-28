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

        //voltar lista de produtos para a view
        public ActionResult Index()
        {
            var produtoes = db.Produtoes.Select(x => x).ToList();

            return View(produtoes);
        }



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

        //public ActionResult AdicionarCarrinho(int id)
        //{
        //    // Ao invés de colocar uma lista de ítens de Design, vamos colocar
        //    // Um objeto da entidade Pedido, que já possui List<ItemDesign>.
        //    // List<ItemDesign> carrinho = Session["Carrinho"] != null ? (List<ItemDesign>)Session["Carrinho"] : new List<ItemDesign>();
        //    Pedido carrinho = Session["Carrinho"] != null ? (Pedido)Session["Carrinho"] : new Pedido();

        //    var produto = db.Produtoes.Find(id);

        //    if (produto != null)
        //    {
        //        var ItemProduto = new ItemProduto();
        //        ItemProduto.Produto = produto;
        //        ItemProduto.Qtd = 1;
        //        // Esta linha não precisa. O EF é espertinho e preenche pra você.
        //        // itemDesign.IdDesign = design.IdDesign;

        //        if (carrinho.ItemProdutos.FirstOrDefault(x => x.ProdutoId == produto.Id) != null)
        //        {
        //            carrinho.ItemProdutos.FirstOrDefault(x => x.ProdutoId == produto.Id).Qtd += 1;
        //        }

        //        else
        //        {
        //            carrinho.ItemProdutos.Add(ItemProduto);
        //        }

        //        // Aqui podemos fazer o cálculo do valor

        //        carrinho.ValorTotal = carrinho.ItemProdutos.Select(i => i.Produto).Sum(d => d.Preco);

        //        Session["Carrinho"] = carrinho;
        //    }

        //    return RedirectToAction("Carrinho");
        //}

        //public ActionResult Carrinho()
        //{
        //    Pedido carrinho = Session["Carrinho"] != null ? (Pedido)Session["Carrinho"] : new Pedido();

        //    return View(carrinho);
        //}
    }
}