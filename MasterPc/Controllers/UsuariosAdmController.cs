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
    [AutorizacaoFilter(Roles = new TipoUsuario[] { TipoUsuario.ADMINISTRADOR })]
    public class UsuariosAdmController : BaseController
    {
        private HomeContext db = new HomeContext();
        private UsuariosAdmDAO dao = new UsuariosAdmDAO();


        [Route ("Admin/Usuario/Listar")]
        public ActionResult Listar()
        {
            var usuarios = db.Usuarios.Select(u => u).ToList();
            return View(usuarios);

        }

        //=========================================\\
        [Route("Admin/Usuario/Criar")]
        public ActionResult Criar()
        {
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "GeneroUsuario");
            return View();
        }

        [HttpPost]
        [Route("Admin/Usuario/Criar")]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "Id,Nome,GeneroId,Login,Senha,CPF,TipoUsuario,Rua,Numero,Bairro,Municipio,Estado,cep,Complemento,Celular")] Usuario usuario)
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
                dao.Adiciona(usuario);
                return RedirectToAction("Listar", "UsuariosAdm");
            }

            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "GeneroUsuario", usuario.GeneroId);
            return View(usuario);
        }

        //=========================================\\
        [Route("Admin/Usuario/Editar/{id}")]
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
        [Route("Admin/Usuario/Editar/{id}")]
        public ActionResult Editar([Bind(Include = "Id,Nome,GeneroId,Login,CPF,Rua,Numero,Bairro,Municipio,Estado,cep,Complemento,Celular,TipoUsuario")] Usuario usuario)
        {
            //Remove a necessidade de editar a senha
            ModelState.Where(c => c.Key.Equals(nameof(usuario.Senha))).ToList().ForEach(c => ModelState.Remove(c));

            //Fazer o sistema que compara o login dele com o banco menos com ele mesmo
            if (db.Usuarios.Where(x => x.Login == usuario.Login && usuario.Id != x.Id).Count() > 0)
            {
                ModelState.AddModelError("Login", "Login existente.");
            }


            //Fazer o sistema que compara o cpf dele com o banco menos com ele mesmo
            if (db.Usuarios.Where(x => x.CPF == usuario.CPF && usuario.Id != x.Id).Count() > 0)
            {
                ModelState.AddModelError("CPF", "CPF já cadastrado");
            }

            if (ModelState.IsValid)
            {
                Usuario up = dao.BuscaPorId(usuario.Id);
                if (string.IsNullOrWhiteSpace(usuario.Senha))
                usuario.Senha = up.Senha;

                dao.Atualiza(usuario);
                return RedirectToAction("Listar");
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "GeneroUsuario", usuario.GeneroId);
            return View(usuario);
        }

        //=========================================\\,
        public ActionResult Deletar(int? id)
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
            return View(usuario);

        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmacaoDeletar(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Listar");
        }

        //=========================================\\
    }
}