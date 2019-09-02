using MasterPc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

public class HomeContext : DbContext
{
    // You can add custom code to this file. Changes will not be overwritten.
    // 
    // If you want Entity Framework to drop and regenerate your database
    // automatically whenever you change your model schema, please use data migrations.
    // For more information refer to the documentation:
    // http://msdn.microsoft.com/en-us/data/jj591621.aspx

    public HomeContext() : base("name=HomeContext")
    {
    }
    public DbSet<Produto> Produtoes { get; set; }
    public DbSet<CategoriaProduto> CategoriaProdutoes { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<CompraProduto> CompraProdutos { get; set; }
}
