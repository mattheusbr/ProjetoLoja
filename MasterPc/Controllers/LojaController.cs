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
        private HomeContext contexto = new HomeContext();

        public ActionResult Catalogo()
        {
            var produtoes = contexto.Produtoes.Select(x => x).ToList();
            return View(produtoes);
        }

        //=========================================\\

        //===Categorias===
        [Route("Produtos/Categoria/{id}")]
        public ActionResult ListarCategoria(int id)
        {
            var categorias = contexto.Produtoes.Select(x => x).Where(x => x.CategoriaId == id).ToList();

            return View(categorias);
        }

        //=========================================\\

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
            produto.Descricao = Regex.Replace(produto.Descricao, Environment.NewLine, "<br />");
            return View(produto);
        }

        //=========================================\\


        //Metodo para salvar uma compra e mandar para o carrinho de compra
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

                _homeContext.Compras.Add(compra);
                item.CompraId = compra.Id;
                _homeContext.CompraProdutos.Add(item);

                _homeContext.SaveChanges();
            }
            else
            {
                CompraProduto item = _homeContext.CompraProdutos.FirstOrDefault(c => c.ProdutoId == id && c.CompraId == compra.Id);
                if (item == null)
                {
                    item = new CompraProduto()
                    {
                        CompraId = compra.Id,
                        ProdutoId = id
                    };
                    _homeContext.CompraProdutos.Add(item);
                }
                item.Quantidade++;
                _homeContext.SaveChanges();
            }

            return RedirectToAction("Compra", "Carrinho");
        }

    }
}