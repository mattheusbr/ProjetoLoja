using MasterPc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterPc.DAO
{
    public class UsuariosDAO
    {
        public void Adiciona(Usuario usuario)
        {
            using (var context = new HomeContext())
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
            }
        }

        public Usuario BuscaPorId(int id)
        {
            using (var contexto = new HomeContext())
            {
                return contexto.Usuarios.Find(id);
            }
        }

        public void Atualiza(Usuario usuario)
        {
            using (var contexto = new HomeContext())
            {
                contexto.Entry(usuario).State = EntityState.Modified;
                contexto.SaveChanges();
            }
        }

        public Usuario Busca(string login, string senha)
        {
            using (var contexto = new HomeContext())
            {
                return contexto.Usuarios.FirstOrDefault(u => u.Login == login && u.Senha == senha);
            }
        }
    }
}