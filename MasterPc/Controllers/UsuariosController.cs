using MasterPc.DAO;
using MasterPc.Filtros;
using MasterPc.Models;
using MasterPc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MasterPc.Controllers
{    
    public class UsuariosController : Controller
    {
        private HomeContext db = new HomeContext();
        private UsuariosDAO dao = new UsuariosDAO();

        public ActionResult Criar()
        {
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "GeneroUsuario");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "Id,Nome,GeneroId,Login,Senha,CPF,Rua,Numero,Bairro,Municipio,Estado,cep,Complemento,Celular")] Usuario usuario)
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
                dao.Adiciona(usuario);
                return RedirectToAction("Logar", "Login");
            }

            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "GeneroUsuario", usuario.GeneroId);
            return View(usuario);
        }

        [AutorizacaoFilter]
        public ActionResult Editar(int? id)
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
        [AutorizacaoFilter]
        public ActionResult Editar([Bind(Include = "Id,Nome,GeneroId,Login,CPF,Rua,Numero,Bairro,Municipio,Estado,cep,Complemento,Celular")] Usuario usuario)
        {
            //Remove a necessidade de editar a senha
            ModelState.Where(c => c.Key.Equals(nameof(usuario.Senha))).ToList().ForEach(c => ModelState.Remove(c));

            //Fazer o sistema que compara o login dele com o banco menos com ele mesmo
            //if (db.Usuarios.Where(x => x.Login == usuario.Login).Count() > 0)
            //{
            //    ModelState.AddModelError("Login", "Login existente.");
            //}

            //Fazer o sistema que compara o cpf dele com o banco menos com ele mesmo
            //if (db.Usuarios.Where(x => x.CPF == usuario.CPF).Count() > 0)
            //{
            //    ModelState.AddModelError("CPF", "CPF já cadastrado");
            //}

            if (ModelState.IsValid)
            {
                Usuario up = dao.BuscaPorId(usuario.Id);
                if (string.IsNullOrWhiteSpace(usuario.Senha))
                    usuario.Senha = up.Senha;
                dao.Atualiza(usuario);
                return RedirectToAction("Catalogo", "Loja");
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
    }
}