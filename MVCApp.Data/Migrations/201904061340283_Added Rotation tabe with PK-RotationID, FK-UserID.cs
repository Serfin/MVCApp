namespace MVCApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRotationtabewithPKRotationIDFKUserID : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rotations",
                c => new
                    {
                        RotationId = c.Guid(nullable: false),
                        Creator = c.Guid(nullable: false),
                        League = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Spots = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RotationId)
                .ForeignKey("dbo.Users", t => t.Creator, cascadeDelete: true)
                .Index(t => t.Creator);
            
            AddColumn("dbo.Users", "Rotation_RotationId", c => c.Guid());
            CreateIndex("dbo.Users", "Rotation_RotationId");
            AddForeignKey("dbo.Users", "Rotation_RotationId", "dbo.Rotations", "RotationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rotations", "Creator", "dbo.Users");
            DropForeignKey("dbo.Users", "Rotation_RotationId", "dbo.Rotations");
            DropIndex("dbo.Users", new[] { "Rotation_RotationId" });
            DropIndex("dbo.Rotations", new[] { "Creator" });
            DropColumn("dbo.Users", "Rotation_RotationId");
            DropTable("dbo.Rotations");
        }
    }
}
