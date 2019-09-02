namespace MasterPc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Home : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoriaProdutoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30),
                        CategoriaId = c.Int(nullable: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantidade = c.Int(nullable: false),
                        Descricao = c.String(nullable: false),
                        Img = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoriaProdutoes", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.CompraProdutoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompraId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Compras", t => t.CompraId, cascadeDelete: true)
                .ForeignKey("dbo.Produtoes", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.CompraId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        GeneroId = c.Int(nullable: false),
                        Senha = c.String(nullable: false, maxLength: 30),
                        Login = c.String(nullable: false),
                        CPF = c.String(nullable: false),
                        Celular = c.String(nullable: false),
                        TipoUsuario = c.Int(nullable: false),
                        Rua = c.String(nullable: false),
                        Numero = c.Int(nullable: false),
                        Bairro = c.String(nullable: false),
                        Municipio = c.String(nullable: false),
                        Estado = c.String(nullable: false),
                        cep = c.String(nullable: false),
                        Complemento = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Generoes", t => t.GeneroId, cascadeDelete: true)
                .Index(t => t.GeneroId);
            
            CreateTable(
                "dbo.Generoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GeneroUsuario = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompraProdutoes", "ProdutoId", "dbo.Produtoes");
            DropForeignKey("dbo.Compras", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "GeneroId", "dbo.Generoes");
            DropForeignKey("dbo.CompraProdutoes", "CompraId", "dbo.Compras");
            DropForeignKey("dbo.Produtoes", "CategoriaId", "dbo.CategoriaProdutoes");
            DropIndex("dbo.Usuarios", new[] { "GeneroId" });
            DropIndex("dbo.Compras", new[] { "UsuarioId" });
            DropIndex("dbo.CompraProdutoes", new[] { "ProdutoId" });
            DropIndex("dbo.CompraProdutoes", new[] { "CompraId" });
            DropIndex("dbo.Produtoes", new[] { "CategoriaId" });
            DropTable("dbo.Generoes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Compras");
            DropTable("dbo.CompraProdutoes");
            DropTable("dbo.Produtoes");
            DropTable("dbo.CategoriaProdutoes");
        }
    }
}
