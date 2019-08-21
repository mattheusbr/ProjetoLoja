namespace MasterPc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class endereco2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enderecoes", "_Endereco", c => c.String(nullable: false));
            DropColumn("dbo.Enderecoes", "Logradouro");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enderecoes", "Logradouro", c => c.String(nullable: false));
            DropColumn("dbo.Enderecoes", "_Endereco");
        }
    }
}
