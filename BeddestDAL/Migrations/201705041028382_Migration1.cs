namespace BeddestDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beds",
                c => new
                    {
                        BedId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10),
                        Temperature = c.Int(nullable: false),
                        AlarmTime = c.DateTime(nullable: false),
                        StopHeatingTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BedId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Blocks",
                c => new
                    {
                        BlockId = c.Int(nullable: false, identity: true),
                        BedId = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        TiltAngle = c.Int(nullable: false),
                        Hardness = c.Int(nullable: false),
                        HardnessLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlockId)
                .ForeignKey("dbo.Beds", t => t.BedId, cascadeDelete: true)
                .Index(t => t.BedId);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        DeviceId = c.Int(nullable: false, identity: true),
                        DeviceName = c.String(nullable: false, maxLength: 20),
                        BedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DeviceId)
                .ForeignKey("dbo.Beds", t => t.BedId, cascadeDelete: true)
                .Index(t => t.BedId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Modes",
                c => new
                    {
                        ModeId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        HeadHeight = c.Int(nullable: false),
                        HeadTilt = c.Int(nullable: false),
                        HeadHardness = c.Int(nullable: false),
                        LegsHeight = c.Int(nullable: false),
                        LegsTilt = c.Int(nullable: false),
                        LegsHardness = c.Int(nullable: false),
                        OtherHeight = c.Int(nullable: false),
                        OtherHardness = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ModeId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Beds", "UserId", "dbo.Users");
            DropForeignKey("dbo.Devices", "BedId", "dbo.Beds");
            DropForeignKey("dbo.Blocks", "BedId", "dbo.Beds");
            DropIndex("dbo.Modes", new[] { "UserId" });
            DropIndex("dbo.Devices", new[] { "BedId" });
            DropIndex("dbo.Blocks", new[] { "BedId" });
            DropIndex("dbo.Beds", new[] { "UserId" });
            DropTable("dbo.Modes");
            DropTable("dbo.Users");
            DropTable("dbo.Devices");
            DropTable("dbo.Blocks");
            DropTable("dbo.Beds");
        }
    }
}
