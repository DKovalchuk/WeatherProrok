namespace WeatherProrok.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cloudities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FactWeathers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Temp = c.Int(nullable: false),
                        Humidity = c.Int(nullable: false),
                        CloudityId = c.Guid(nullable: false),
                        PrecipitationId = c.Guid(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cloudities", t => t.CloudityId, cascadeDelete: true)
                .ForeignKey("dbo.Precipitations", t => t.PrecipitationId, cascadeDelete: true)
                .Index(t => t.CloudityId)
                .Index(t => t.PrecipitationId);
            
            CreateTable(
                "dbo.Precipitations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForecastWeathers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Temp = c.Int(nullable: false),
                        Humidity = c.Int(nullable: false),
                        ForecastTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FactWeathers", "PrecipitationId", "dbo.Precipitations");
            DropForeignKey("dbo.FactWeathers", "CloudityId", "dbo.Cloudities");
            DropIndex("dbo.FactWeathers", new[] { "PrecipitationId" });
            DropIndex("dbo.FactWeathers", new[] { "CloudityId" });
            DropTable("dbo.ForecastWeathers");
            DropTable("dbo.Precipitations");
            DropTable("dbo.FactWeathers");
            DropTable("dbo.Cloudities");
        }
    }
}
