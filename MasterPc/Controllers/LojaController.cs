using MasterPc.Filtros;
using MasterPc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MasterPc.Controllers
{
    public class LojaController : BaseController
    {

        public ActionResult Catalogo()
        {
            var produtoes = _homeContext.Produtoes.Select(x => x).ToList();
            return View(produtoes);
        }

        [Route("Produtos/Categoria/{id}")]
        public ActionResult ListarCategoria(int id)
        {
            var categorias = _homeContext.Produtoes.Select(x => x).Where(x => x.CategoriaId == id).ToList();

            return View(categorias);
        }


        public ActionResult ProdutoDetalhar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = _homeContext.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            produto.Descricao = Regex.Replace(produto.Descricao, Environment.NewLine, "<br />");
            return View(produto);
        }

        [AutorizacaoFilter]
        public ActionResult AdicionarCompra(int id, int quantidade)
        {
            Usuario usuario = new Usuario();
            if (Session["usuarioLogado"] != null)
            {
                usuario = Session["usuarioLogado"] as Usuario;
            }
            Compra compra = _homeContext.Compras.Where(x => x.Status == Types.StatusCompra.Aberto && x.UsuarioId == usuario.Id).FirstOrDefault();
            if (compra == null)
            {
                compra = new Compra();
                compra.UsuarioId = usuario.Id;
                compra.Status = Types.StatusCompra.Aberto;
                compra.Data = DateTime.Now;
                CompraProduto item = new CompraProduto();
                item.ProdutoId = id;
                item.Quantidade = quantidade;

                base._homeContext.Compras.Add(compra);
                item.CompraId = compra.Id;
                base._homeContext.CompraProdutos.Add(item);

                base._homeContext.SaveChanges();
            }
            else
            {
                CompraProduto item = base._homeContext.CompraProdutos.FirstOrDefault(c => c.ProdutoId == id && c.CompraId == compra.Id);
                if (item == null)
                {
                    item = new CompraProduto()
                    {
                        CompraId = compra.Id,
                        ProdutoId = id
                    };
                    base._homeContext.CompraProdutos.Add(item);
                }
                item.Quantidade++;
                base._homeContext.SaveChanges();
            }
            return RedirectToAction("Compra", "Carrinho");
        }
    }
}