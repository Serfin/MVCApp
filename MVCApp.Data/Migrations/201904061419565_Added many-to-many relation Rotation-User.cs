namespace MVCApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedmanytomanyrelationRotationUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Rotation_RotationId", "dbo.Rotations");
            DropIndex("dbo.Users", new[] { "Rotation_RotationId" });
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        RotationId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RotationId, t.UserId })
                .ForeignKey("dbo.Rotations", t => t.RotationId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.RotationId)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Users", "Rotation_RotationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Rotation_RotationId", c => c.Guid());
            DropForeignKey("dbo.Members", "UserId", "dbo.Users");
            DropForeignKey("dbo.Members", "RotationId", "dbo.Rotations");
            DropIndex("dbo.Members", new[] { "UserId" });
            DropIndex("dbo.Members", new[] { "RotationId" });
            DropTable("dbo.Members");
            CreateIndex("dbo.Users", "Rotation_RotationId");
            AddForeignKey("dbo.Users", "Rotation_RotationId", "dbo.Rotations", "RotationId");
        }
    }
}
