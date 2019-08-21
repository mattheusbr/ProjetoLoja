namespace MasterPc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hom : DbMigration
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
                        Preco = c.Double(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        Descricao = c.String(nullable: false),
                        Img = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoriaProdutoes", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Generoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GeneroUsuario = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        GeneroId = c.Int(nullable: false),
                        Senha = c.String(nullable: false, maxLength: 30),
                        Login = c.String(nullable: false),
                        CPF = c.String(nullable: false),
                        TipoUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Generoes", t => t.GeneroId, cascadeDelete: true)
                .Index(t => t.GeneroId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "GeneroId", "dbo.Generoes");
            DropForeignKey("dbo.Produtoes", "CategoriaId", "dbo.CategoriaProdutoes");
            DropIndex("dbo.Usuarios", new[] { "GeneroId" });
            DropIndex("dbo.Produtoes", new[] { "CategoriaId" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Generoes");
            DropTable("dbo.Produtoes");
            DropTable("dbo.CategoriaProdutoes");
        }
    }
}
