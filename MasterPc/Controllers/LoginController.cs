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

namespace MasterPc.Controllers
{
    public class LoginController : Controller
    {
        private HomeContext db = new HomeContext();

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
        // GET: Login/Create
        public ActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "GeneroUsuario");
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,GeneroId,Login,Senha,CPF")] Usuario usuario)
        {
            //Consulta no banco se o login existe
            if (db.Usuarios.Where(x => x.Login == usuario.Login).Count() > 0)
            {
                ModelState.AddModelError("Login", "Login existente.");
            }
            if (ModelState.IsValid)
            {
                //Seta como padrão o usuario na tabela
                usuario.TipoUsuario = (int)TipoUsuario.USUARIO;
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
        public ActionResult Edit([Bind(Include = "Id,Nome,GeneroId,Login,CPF,TipoUsuario")] Usuario usuario)
        {
            //Remove a necessidade de editar a senha
            ModelState.Where(c => c.Key.Equals(nameof(usuario.Senha))).ToList().ForEach(c => ModelState.Remove(c));

            if (ModelState.IsValid)
            {
                UsuariosDAO dao = new UsuariosDAO();
                Usuario up = dao.BuscaPorId(usuario.ID);
                if (string.IsNullOrWhiteSpace(usuario.Senha))
                usuario.Senha = up.Senha;

                //Salva
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "GeneroUsuario", usuario.GeneroId);
            return View(usuario);
        }

        public ActionResult Endereco()
        {
            return View();
        }

        //Criar endereço
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Endereco (Endereco endereco)
        {

            if (ModelState.IsValid)
            {
                db.Enderecos.Add(endereco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(endereco);
        }


    }
}

    