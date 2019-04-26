namespace MVCApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixeddatabasedataformat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rotations", "League", c => c.String());
            AlterColumn("dbo.Rotations", "Type", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rotations", "Type", c => c.Int(nullable: false));
            AlterColumn("dbo.Rotations", "League", c => c.Int(nullable: false));
        }
    }
}
