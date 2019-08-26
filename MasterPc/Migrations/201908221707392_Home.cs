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
                        Preco = c.Double(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        Descricao = c.String(nullable: false),
                        Img = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoriaProdutoes", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Enderecoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rua = c.String(nullable: false),
                        Numero = c.Int(nullable: false),
                        Bairro = c.String(nullable: false),
                        Municipio = c.String(nullable: false),
                        Estado = c.String(nullable: false),
                        cep = c.String(nullable: false),
                        Complemento = c.String(nullable: false),
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
                        EnderecoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
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
            
            CreateTable(
                "dbo.UsuarioEnderecoes",
                c => new
                    {
                        Usuario_ID = c.Int(nullable: false),
                        Endereco_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Usuario_ID, t.Endereco_Id })
                .ForeignKey("dbo.Usuarios", t => t.Usuario_ID, cascadeDelete: true)
                .ForeignKey("dbo.Enderecoes", t => t.Endereco_Id, cascadeDelete: true)
                .Index(t => t.Usuario_ID)
                .Index(t => t.Endereco_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "GeneroId", "dbo.Generoes");
            DropForeignKey("dbo.UsuarioEnderecoes", "Endereco_Id", "dbo.Enderecoes");
            DropForeignKey("dbo.UsuarioEnderecoes", "Usuario_ID", "dbo.Usuarios");
            DropForeignKey("dbo.Produtoes", "CategoriaId", "dbo.CategoriaProdutoes");
            DropIndex("dbo.UsuarioEnderecoes", new[] { "Endereco_Id" });
            DropIndex("dbo.UsuarioEnderecoes", new[] { "Usuario_ID" });
            DropIndex("dbo.Usuarios", new[] { "GeneroId" });
            DropIndex("dbo.Produtoes", new[] { "CategoriaId" });
            DropTable("dbo.UsuarioEnderecoes");
            DropTable("dbo.Generoes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Enderecoes");
            DropTable("dbo.Produtoes");
            DropTable("dbo.CategoriaProdutoes");
        }
    }
}
