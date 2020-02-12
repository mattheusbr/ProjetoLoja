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
    public class UsuariosController : BaseController
    {

        public ActionResult Criar()
        {
            ViewBag.GeneroId = new SelectList(_homeContext.Generos, "Id", "GeneroUsuario");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "Id,Nome,GeneroId,Login,Senha,CPF,Rua,Numero,Bairro,Municipio,Estado,cep,Complemento,Celular")] Usuario usuario)
        {
            if (_homeContext.Usuarios.Where(x => x.Login == usuario.Login).Count() > 0)
            {
                ModelState.AddModelError("Login", "Login existente.");
            }
            if (_homeContext.Usuarios.Where(x => x.CPF == usuario.CPF).Count() > 0)
            {
                ModelState.AddModelError("CPF", "CPF já cadastrado");
            }

            if (ModelState.IsValid)
            {
                usuario.TipoUsuario = TipoUsuario.USUARIO;

                _usuariodao.Adiciona(usuario);
                return RedirectToAction("Logar", "Login");
            }

            ViewBag.GeneroId = new SelectList(_homeContext.Generos, "Id", "GeneroUsuario", usuario.GeneroId);
            return View(usuario);
        }

        [AutorizacaoFilter]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = _homeContext.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneroId = new SelectList(_homeContext.Generos, "Id", "GeneroUsuario", usuario.GeneroId);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutorizacaoFilter]
        public ActionResult Editar([Bind(Include = "Id,Nome,GeneroId,Login,CPF,Rua,Numero,Bairro,Municipio,Estado,cep,Complemento,Celular")] Usuario usuario)
        {
            //Remove a necessidade de editar a senha
            ModelState.Where(c => c.Key.Equals(nameof(usuario.Senha))).ToList().ForEach(c => ModelState.Remove(c));

            if (_homeContext.Usuarios.Where(x => x.Login == usuario.Login && usuario.Id != x.Id).Count() > 0)
            {
                ModelState.AddModelError("Login", "Login existente.");
            }

            if (_homeContext.Usuarios.Where(x => x.CPF == usuario.CPF && usuario.Id != x.Id).Count() > 0)
            {
                ModelState.AddModelError("CPF", "CPF já cadastrado");
            }

            if (ModelState.IsValid)
            {
                usuario.TipoUsuario = TipoUsuario.USUARIO;
                Usuario up = _usuariodao.BuscaPorId(usuario.Id);
                if (string.IsNullOrWhiteSpace(usuario.Senha))
                    usuario.Senha = up.Senha;
                _usuariodao.Atualiza(usuario);
                return RedirectToAction("Catalogo", "Loja");
            }
            ViewBag.GeneroId = new SelectList(_homeContext.Generos, "Id", "GeneroUsuario", usuario.GeneroId);
            return View(usuario);
        }
    }
}