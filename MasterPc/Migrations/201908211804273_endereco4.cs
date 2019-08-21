namespace MasterPc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class endereco4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enderecoes", "Usuario_ID", "dbo.Usuarios");
            DropIndex("dbo.Enderecoes", new[] { "Usuario_ID" });
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
            
            DropColumn("dbo.Enderecoes", "Usuario_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enderecoes", "Usuario_ID", c => c.Int());
            DropForeignKey("dbo.UsuarioEnderecoes", "Endereco_Id", "dbo.Enderecoes");
            DropForeignKey("dbo.UsuarioEnderecoes", "Usuario_ID", "dbo.Usuarios");
            DropIndex("dbo.UsuarioEnderecoes", new[] { "Endereco_Id" });
            DropIndex("dbo.UsuarioEnderecoes", new[] { "Usuario_ID" });
            DropTable("dbo.UsuarioEnderecoes");
            CreateIndex("dbo.Enderecoes", "Usuario_ID");
            AddForeignKey("dbo.Enderecoes", "Usuario_ID", "dbo.Usuarios", "ID");
        }
    }
}
