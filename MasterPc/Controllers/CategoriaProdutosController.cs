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
    [AutorizacaoFilter(Roles = new TipoUsuario[] { TipoUsuario.ADMINISTRADOR })]
    public class CategoriaProdutosController : BaseController
    {
        private HomeContext db = new HomeContext();

        [Route ("Categoria/Listar")]
        public ActionResult Listar()
        {
            return View(db.CategoriaProdutoes.ToList());
        }

        //=========================================\\
        [Route("Categoria/Detalhar/{id}")]
        public ActionResult Detalhar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaProduto categoriaProduto = db.CategoriaProdutoes.Find(id);
            if (categoriaProduto == null)
            {
                return HttpNotFound();
            }
            return View(categoriaProduto);
        }

        //=========================================\\
        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Criar([Bind(Include = "Id,Nome,Descricao")] CategoriaProduto categoriaProduto)
        {
            if (ModelState.IsValid)
            {
                db.CategoriaProdutoes.Add(categoriaProduto);
                db.SaveChanges();
                return RedirectToAction("Listar");
            }

            return View(categoriaProduto);
        }

        //=========================================\\
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaProduto categoriaProduto = db.CategoriaProdutoes.Find(id);
            if (categoriaProduto == null)
            {
                return HttpNotFound();
            }
            return View(categoriaProduto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,Descricao")] CategoriaProduto categoriaProduto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriaProduto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Listar");
            }
            return View(categoriaProduto);
        }

        //=========================================\\
        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaProduto categoriaProduto = db.CategoriaProdutoes.Find(id);
            if (categoriaProduto == null)
            {
                return HttpNotFound();
            }
            return View(categoriaProduto);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarConfimar(int id)
        {
            CategoriaProduto categoriaProduto = db.CategoriaProdutoes.Find(id);
            db.CategoriaProdutoes.Remove(categoriaProduto);
            db.SaveChanges();
            return RedirectToAction("Listar");
        }

        //=========================================\\
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
