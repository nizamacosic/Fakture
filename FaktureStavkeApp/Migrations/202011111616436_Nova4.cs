namespace FaktureStavkeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nova4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faktures", "DatumStvaranja", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Faktures", "DatumStvaranja");
        }
    }
}
