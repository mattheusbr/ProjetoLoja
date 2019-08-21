using MasterPc.Models;
using MasterPc.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MasterPc.Filtros
{
    public class AutorizacaoFilterAttribute : ActionFilterAttribute
    {
        public TipoUsuario[] Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Setando isLogin como false
            bool isLogin = false;
            object usuario = filterContext.HttpContext.Session["usuarioLogado"];
            if(usuario != null && usuario is Usuario)
            {
                //TODO comparar Roles com os tipos do usuario dentro do registro
                if(Roles != null && Roles.Length > 0)
                {
                    TipoUsuario tipo = (TipoUsuario)(usuario as Usuario).TipoUsuario;
                    int indexOf = Array.IndexOf(Roles, tipo);
                    if ( indexOf > -1)
                    {
                        isLogin = true;
                    }
                }
                else
                {
                    isLogin = true;
                }
            }

            //Se metodo isLogin for falso vai redirecionar para a index novamente.
            if (!isLogin)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Login",
                            action = "Index"
                        }));
            }
        }
        
    }
}
