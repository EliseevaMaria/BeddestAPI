namespace BeddestDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceKeyAdding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "ClientDeviceKey", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "ClientDeviceKey");
        }
    }
}
