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
    public class ProdutoesController : Controller
    {
        private HomeContext db = new HomeContext();

        // GET: Produtoes
        public ActionResult Index()
        {
            var produtoes = db.Produtoes.Select(x => x).ToList();
            
            return View(produtoes);
        }

        // GET: Produtoes/Details/5
        public ActionResult Details(int? id)
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

        // GET: Produtoes/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.CategoriaProdutoes, "Id", "Nome");
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,CategoriaId,Preco,Quantidade,Descricao")] Produto produto)
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
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.CategoriaProdutoes, "Id", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtoes/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Produtoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,CategoriaId,Preco,Quantidade,Descricao")] Produto produto)
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
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.CategoriaProdutoes, "Id", "Nome", produto.CategoriaId);
            return View(produto);
        }
        
        // GET: Produtoes/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Produtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtoes.Find(id);
            db.Produtoes.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
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
