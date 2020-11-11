namespace FaktureStavkeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Druga : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Faktures", "KorisnikId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Faktures", "KorisnikId", c => c.String());
        }
    }
}
