namespace FaktureStavkeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faktures", "UkupnaCijenaBezPDV", c => c.Double(nullable: false));
            AddColumn("dbo.Faktures", "UkupnaCijenaPDV", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Faktures", "UkupnaCijenaPDV");
            DropColumn("dbo.Faktures", "UkupnaCijenaBezPDV");
        }
    }
}
