namespace FaktureStavkeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nova : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faktures", "IznosPorezaUPostotcima", c => c.Double(nullable: false));
            DropColumn("dbo.Faktures", "UkupnaCijenaBezPDV");
            DropColumn("dbo.Faktures", "UkupnaCijenaPDV");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Faktures", "UkupnaCijenaPDV", c => c.Double(nullable: false));
            AddColumn("dbo.Faktures", "UkupnaCijenaBezPDV", c => c.Double(nullable: false));
            DropColumn("dbo.Faktures", "IznosPorezaUPostotcima");
        }
    }
}
