namespace MasterPc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class endereco3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enderecoes", "Usuario_ID", c => c.Int());
            AddColumn("dbo.Usuarios", "EnderecoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Enderecoes", "Usuario_ID");
            AddForeignKey("dbo.Enderecoes", "Usuario_ID", "dbo.Usuarios", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enderecoes", "Usuario_ID", "dbo.Usuarios");
            DropIndex("dbo.Enderecoes", new[] { "Usuario_ID" });
            DropColumn("dbo.Usuarios", "EnderecoId");
            DropColumn("dbo.Enderecoes", "Usuario_ID");
        }
    }
}
