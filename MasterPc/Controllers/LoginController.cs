using MasterPc.DAO;
using MasterPc.Models;
using MasterPc.Types;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MasterPc.Controllers
{
    public class LoginController : Controller
    {
        private HomeContext db = new HomeContext();
        private UsuariosDAO dao = new UsuariosDAO();


        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Autentica(String login, String senha)
        {
            UsuariosDAO dao = new UsuariosDAO();
            Usuario usuario = dao.Busca(login, senha);
            if (usuario != null)
            {
                Session["usuarioLogado"] = usuario;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Login/Create
        public ActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "GeneroUsuario");
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,GeneroId,Login,Senha,CPF,Rua,Numero,Bairro,Municipio,Estado,cep,Complemento,Celular")] Usuario usuario)
        {
            //Consulta no banco se o login existe
            if (db.Usuarios.Where(x => x.Login == usuario.Login).Count() > 0)
            {
                ModelState.AddModelError("Login", "Login existente.");
            }
            if (db.Usuarios.Where(x => x.CPF == usuario.CPF).Count() > 0)
            {
                ModelState.AddModelError("CPF", "CPF já cadastrado");
            }

            if (ModelState.IsValid)
            {
                //Seta como padrão o usuario na tabela
                usuario.TipoUsuario = TipoUsuario.USUARIO;
                //
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        public ActionResult Edit([Bind(Include = "Id,Nome,GeneroId,Login,CPF,Rua,Numero,Bairro,Municipio,Estado,cep,Complemento,Celular")] Usuario usuario)
        {
            //Remove a necessidade de editar a senha
            ModelState.Where(c => c.Key.Equals(nameof(usuario.Senha))).ToList().ForEach(c => ModelState.Remove(c));

            if (ModelState.IsValid)
            {
                Usuario up = dao.BuscaPorId(usuario.Id);
                if (string.IsNullOrWhiteSpace(usuario.Senha))
                usuario.Senha = up.Senha;
                dao.Atualiza(usuario);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "GeneroUsuario", usuario.GeneroId);
            return View(usuario);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
  

    }
}

    