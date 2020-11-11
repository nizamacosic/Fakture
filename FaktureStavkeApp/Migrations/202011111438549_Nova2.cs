namespace FaktureStavkeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nova2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faktures", "PrimateljRacuna", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Faktures", "PrimateljRacuna");
        }
    }
}
