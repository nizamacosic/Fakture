namespace FaktureStavkeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nova3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Faktures", name: "Id", newName: "KorisnikId");
            RenameIndex(table: "dbo.Faktures", name: "IX_Id", newName: "IX_KorisnikId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Faktures", name: "IX_KorisnikId", newName: "IX_Id");
            RenameColumn(table: "dbo.Faktures", name: "KorisnikId", newName: "Id");
        }
    }
}
