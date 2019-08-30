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
    public class ProdutosController : Controller
    {
        private HomeContext db = new HomeContext();

        public ActionResult Listar()
        {
            var produtoes = db.Produtoes.Select(x => x).ToList();
            
            
            return View(produtoes);
        }

        public ActionResult Detalhar(int? id)
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

        public ActionResult Criar()
        {
            ViewBag.CategoriaId = new SelectList(db.CategoriaProdutoes, "Id", "Nome");
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
                //add e salva produto
                db.Produtoes.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Listar");
            }

            ViewBag.CategoriaId = new SelectList(db.CategoriaProdutoes, "Id", "Nome", produto.CategoriaId);
            return View(produto);
        }

        public ActionResult Editar(int? id)
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
            ViewBag.CategoriaId = new SelectList(db.CategoriaProdutoes, "Id", "Nome", produto.CategoriaId);
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
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Listar");
            }
            ViewBag.CategoriaId = new SelectList(db.CategoriaProdutoes, "Id", "Nome", produto.CategoriaId);
            return View(produto);
        }
        
        public ActionResult Deletar(int? id)
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

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarConfirmado(int id)
        {
            Produto produto = db.Produtoes.Find(id);
            db.Produtoes.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Listar");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
