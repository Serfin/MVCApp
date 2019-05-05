namespace MVCApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedcreatorIgnfieldtoRotationtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rotations", "CreatorIgn", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rotations", "CreatorIgn");
        }
    }
}
