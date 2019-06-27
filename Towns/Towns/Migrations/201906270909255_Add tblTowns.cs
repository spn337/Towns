namespace Towns.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtblTowns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblTowns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        TownTypeId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblRegions", t => t.RegionId, cascadeDelete: true)
                .ForeignKey("dbo.tblTownTypes", t => t.TownTypeId, cascadeDelete: true)
                .Index(t => t.TownTypeId)
                .Index(t => t.RegionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblTowns", "TownTypeId", "dbo.tblTownTypes");
            DropForeignKey("dbo.tblTowns", "RegionId", "dbo.tblRegions");
            DropIndex("dbo.tblTowns", new[] { "RegionId" });
            DropIndex("dbo.tblTowns", new[] { "TownTypeId" });
            DropTable("dbo.tblTowns");
        }
    }
}
