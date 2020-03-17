using MasterPc.Filtros;
using MasterPc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterPc.Controllers
{
    [AutorizacaoFilter]
    public class CarrinhoController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Compra()
        {
            Usuario usuario = new Usuario();
            if (Session["usuarioLogado"] != null) {
                usuario = Session["usuarioLogado"] as Usuario;
            }
            Compra compra =  _homeContext.Compras.Where(x => x.Status == Types.StatusCompra.Aberto && x.UsuarioId == usuario.Id).FirstOrDefault();
            if (compra == null) compra = new Compra();
            var ListaElemento = _homeContext.CompraProdutos.Select(x => x).Where(x => x.CompraId == compra.Id).ToList();

            compra.CompraProdutos = ListaElemento;

            foreach (var item in compra.CompraProdutos)
            {
                item.Produto = _homeContext.Produtoes.FirstOrDefault(c => c.Id == item.ProdutoId);
            }
            return View(compra);
        }

        public ActionResult Finalizar(int id)
        {

            Usuario usuario = new Usuario();
            if (Session["usuarioLogado"] != null)
            {
                usuario = Session["usuarioLogado"] as Usuario;
            }
            Compra compra = _homeContext.Compras.Where(x => x.Status == Types.StatusCompra.Aberto && x.UsuarioId == usuario.Id).FirstOrDefault();
            if (compra == null) compra = new Compra();
            var ListaElemento = _homeContext.CompraProdutos.Select(x => x).Where(x => x.CompraId == compra.Id).ToList();

            compra.CompraProdutos = ListaElemento;

            foreach (var item in compra.CompraProdutos)
            {
                Produto produto = _homeContext.Produtoes.FirstOrDefault(c => c.Id == item.ProdutoId);
                produto.Quantidade = produto.Quantidade - item.Quantidade;
            }
            compra.Status = Types.StatusCompra.Fechado;
            _homeContext.SaveChanges();

            return RedirectToAction("Catalogo", "Loja");
        }

        public ActionResult RemoverItem(int id)
        {

            Usuario usuario = new Usuario();
            if (Session["usuarioLogado"] != null)
            {
                usuario = Session["usuarioLogado"] as Usuario;
            }
            Compra compra = _homeContext.Compras.Where(x => x.Status == Types.StatusCompra.Aberto && x.UsuarioId == usuario.Id).FirstOrDefault();
            if (compra == null)
            {
                return RedirectToAction("Catalogo", "Loja");
            }

            CompraProduto item = this._homeContext.CompraProdutos.FirstOrDefault(c => c.Id == id);
            _homeContext.CompraProdutos.Remove(item);
            _homeContext.SaveChanges();
            return RedirectToAction("Compra", "Carrinho");
        }
    }
}