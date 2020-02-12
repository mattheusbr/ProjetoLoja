using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MasterPc.Filtros;
using MasterPc.Models;
using MasterPc.Types;

namespace MasterPc.Controllers
{
    [AutorizacaoFilter(Roles = new TipoUsuario[] { TipoUsuario.ADMINISTRADOR} )]
    public class ProdutosController : BaseController
    {

        public ActionResult Listar()
        {
            var produtoes = _homeContext.Produtoes.Select(x => x).ToList();
            
            
            return View(produtoes);
        }

        public ActionResult Detalhar(int? id)
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
            return View(produto);
        }

        public ActionResult Criar()
        {
            ViewBag.CategoriaId = new SelectList(_homeContext.CategoriaProdutoes, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "Id,Nome,CategoriaId,Preco,Quantidade,Descricao")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                //Converte de inputStream para base64
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];                    
                    byte[] buffer = new byte[file.InputStream.Length];
                    file.InputStream.Read(buffer, 0, buffer.Length);
                    produto.Img =  Convert.ToBase64String(buffer);
                }
                _homeContext.Produtoes.Add(produto);
                _homeContext.SaveChanges();
                return RedirectToAction("Listar");
            }

            ViewBag.CategoriaId = new SelectList(_homeContext.CategoriaProdutoes, "Id", "Nome", produto.CategoriaId);
            return View(produto);
        }

        public ActionResult Editar(int? id)
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
            ViewBag.CategoriaId = new SelectList(_homeContext.CategoriaProdutoes, "Id", "Nome", produto.CategoriaId);
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,CategoriaId,Preco,Quantidade,Descricao")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    //Converte de inputStream para base64 para salvar no banco
                    HttpPostedFileBase file = Request.Files[0];
                    byte[] buffer = new byte[file.InputStream.Length];
                    file.InputStream.Read(buffer, 0, buffer.Length);
                    produto.Img = Convert.ToBase64String(buffer);
                }
                _homeContext.Entry(produto).State = EntityState.Modified;
                _homeContext.SaveChanges();
                return RedirectToAction("Listar");
            }
            ViewBag.CategoriaId = new SelectList(_homeContext.CategoriaProdutoes, "Id", "Nome", produto.CategoriaId);
            return View(produto);
        }
        
        public ActionResult Deletar(int? id)
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
            return View(produto);

        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarConfirmado(int id)
        {
            Produto produto = _homeContext.Produtoes.Find(id);
            _homeContext.Produtoes.Remove(produto);
            _homeContext.SaveChanges();
            return RedirectToAction("Listar");
        }
        
        public JsonResult Add(int id)
        {            
            Produto produto = _homeContext.Produtoes.Find(id);
            if (produto == null)
            {
                Response.StatusCode = 500;
            }
            else
            {
                produto.Quantidade++;
                _homeContext.Entry(produto).State = EntityState.Modified;
                _homeContext.SaveChanges();
            }            
            return Json(produto.Quantidade, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Sub(int id)
        {
            Produto produto = _homeContext.Produtoes.Find(id);
            if (produto == null)
            {
                Response.StatusCode = 500;
            }
            else if(produto.Quantidade > 0)
            {
                produto.Quantidade--;
                _homeContext.Entry(produto).State = EntityState.Modified;
                _homeContext.SaveChanges();
            }
            return Json(produto.Quantidade, JsonRequestBehavior.AllowGet);
        }
    }
}
