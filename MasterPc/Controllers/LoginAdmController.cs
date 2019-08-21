using MasterPc.DAO;
using MasterPc.Filtros;
using MasterPc.Models;
using MasterPc.Types;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MasterPc.Controllers
{
    //[AutorizacaoFilter(Roles = new TipoUsuario[] { TipoUsuario.ADMINISTRADOR })]
    public class LoginAdmController : Controller
    {
        private HomeContext db = new HomeContext();

        // GET: Login/Create

        public ActionResult Lista()
        {
            var usuarios = db.Usuarios.Select(u => u).ToList();
            return View(usuarios);

        }
        public ActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "GeneroUsuario");
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,GeneroId,Login,Senha,CPF,TipoUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index", "Produtoes");
            }

            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "GeneroUsuario", usuario.GeneroId);
            return View(usuario);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Editar
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "GeneroUsuario", usuario.GeneroId);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,GeneroId,Login,CPF,TipoUsuario")] Usuario usuario)
        {
            ModelState.Where(c => c.Key.Equals(nameof(usuario.Senha))).ToList().ForEach(c => ModelState.Remove(c));
            if (ModelState.IsValid)
            {
                UsuariosDAO dao = new UsuariosDAO();
                Usuario up = dao.BuscaPorId(usuario.ID);
                if (string.IsNullOrWhiteSpace(usuario.Senha))
                    usuario.Senha = up.Senha;

                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "GeneroUsuario", usuario.GeneroId);
            return View(usuario);
        }
    }
}
