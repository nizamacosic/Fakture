namespace FaktureStavkeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Treca : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Faktures", "FakturaID", "dbo.AspNetUsers");
            DropForeignKey("dbo.FakturaStavkas", "FakturaID", "dbo.Faktures");
            DropIndex("dbo.Faktures", new[] { "FakturaID" });
            DropPrimaryKey("dbo.Faktures");
            AddColumn("dbo.Faktures", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Faktures", "FakturaID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Faktures", "FakturaID");
            CreateIndex("dbo.Faktures", "Id");
            AddForeignKey("dbo.Faktures", "Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FakturaStavkas", "FakturaID", "dbo.Faktures", "FakturaID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FakturaStavkas", "FakturaID", "dbo.Faktures");
            DropForeignKey("dbo.Faktures", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Faktures", new[] { "Id" });
            DropPrimaryKey("dbo.Faktures");
            AlterColumn("dbo.Faktures", "FakturaID", c => c.Int(nullable: false));
            DropColumn("dbo.Faktures", "Id");
            AddPrimaryKey("dbo.Faktures", "FakturaID");
            CreateIndex("dbo.Faktures", "FakturaID");
            AddForeignKey("dbo.FakturaStavkas", "FakturaID", "dbo.Faktures", "FakturaID", cascadeDelete: true);
            AddForeignKey("dbo.Faktures", "FakturaID", "dbo.AspNetUsers", "Id");
        }
    }
}
