namespace MasterPc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class endereco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enderecoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Logradouro = c.String(nullable: false),
                        Numero = c.Int(nullable: false),
                        Bairro = c.String(nullable: false),
                        Municipio = c.String(nullable: false),
                        Estado = c.String(nullable: false),
                        CEP = c.String(nullable: false),
                        Complemento = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Enderecoes");
        }
    }
}
