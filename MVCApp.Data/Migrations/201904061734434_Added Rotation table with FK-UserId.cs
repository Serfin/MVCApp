namespace MVCApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRotationtablewithFKUserId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rotations",
                c => new
                    {
                        RotationId = c.Guid(nullable: false),
                        Creator = c.Guid(nullable: false),
                        League = c.String(),
                        Type = c.String(),
                        Spots = c.Int(),
                        CreatedAt = c.DateTime(nullable: false),
                        User_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.RotationId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Users", t => t.Creator, cascadeDelete: true)
                .Index(t => t.Creator)
                .Index(t => t.User_UserId);
            
            AddColumn("dbo.Users", "Rotation_RotationId", c => c.Guid());
            CreateIndex("dbo.Users", "Rotation_RotationId");
            AddForeignKey("dbo.Users", "Rotation_RotationId", "dbo.Rotations", "RotationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rotations", "Creator", "dbo.Users");
            DropForeignKey("dbo.Users", "Rotation_RotationId", "dbo.Rotations");
            DropForeignKey("dbo.Rotations", "User_UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Rotation_RotationId" });
            DropIndex("dbo.Rotations", new[] { "User_UserId" });
            DropIndex("dbo.Rotations", new[] { "Creator" });
            DropColumn("dbo.Users", "Rotation_RotationId");
            DropTable("dbo.Rotations");
        }
    }
}
