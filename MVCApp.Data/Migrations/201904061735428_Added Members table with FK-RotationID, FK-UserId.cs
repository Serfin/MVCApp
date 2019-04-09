namespace MVCApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMemberstablewithFKRotationIDFKUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rotations", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "Rotation_RotationId", "dbo.Rotations");
            DropIndex("dbo.Rotations", new[] { "User_UserId" });
            DropIndex("dbo.Users", new[] { "Rotation_RotationId" });
            CreateTable(
                "dbo.RotationMembers",
                c => new
                    {
                        RotationId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RotationId, t.UserId })
                .ForeignKey("dbo.Rotations", t => t.RotationId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.RotationId)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Rotations", "User_UserId");
            DropColumn("dbo.Users", "Rotation_RotationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Rotation_RotationId", c => c.Guid());
            AddColumn("dbo.Rotations", "User_UserId", c => c.Guid());
            DropForeignKey("dbo.RotationMembers", "UserId", "dbo.Users");
            DropForeignKey("dbo.RotationMembers", "RotationId", "dbo.Rotations");
            DropIndex("dbo.RotationMembers", new[] { "UserId" });
            DropIndex("dbo.RotationMembers", new[] { "RotationId" });
            DropTable("dbo.RotationMembers");
            CreateIndex("dbo.Users", "Rotation_RotationId");
            CreateIndex("dbo.Rotations", "User_UserId");
            AddForeignKey("dbo.Users", "Rotation_RotationId", "dbo.Rotations", "RotationId");
            AddForeignKey("dbo.Rotations", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
