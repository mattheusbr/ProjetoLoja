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
    public class LoginController : BaseController
    {
        HomeContext contexto = new HomeContext();
        private UsuariosDAO dao = new UsuariosDAO();

        // GET: /Login/
        public ActionResult Logar()
        {
            return View();
        }

        public ActionResult Autentica(String login, String senha)
        {
            Usuario usuario = dao.Busca(login, senha);
            if (usuario != null)
            {
                Session["usuarioLogado"] = usuario;
                return RedirectToAction("Catalogo", "Loja");
            }
            else
            {
                return RedirectToAction("Logar");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.Abandon();
            return RedirectToAction("Logar", "Login");
        }
    }
}

    