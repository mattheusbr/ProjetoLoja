namespace MasterPc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class endereco5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enderecoes", "Rua", c => c.String(nullable: false));
            AddColumn("dbo.Enderecoes", "Resultado", c => c.String());
            DropColumn("dbo.Enderecoes", "_Endereco");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enderecoes", "_Endereco", c => c.String(nullable: false));
            DropColumn("dbo.Enderecoes", "Resultado");
            DropColumn("dbo.Enderecoes", "Rua");
        }
    }
}
