namespace WeatherProrok.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKeys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CityProviderId = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.FactWeathers", "CityId", c => c.Guid(nullable: false));
            AddColumn("dbo.ForecastWeathers", "CityId", c => c.Guid(nullable: false));
            CreateIndex("dbo.FactWeathers", "CityId");
            CreateIndex("dbo.ForecastWeathers", "CityId");
            AddForeignKey("dbo.FactWeathers", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ForecastWeathers", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForecastWeathers", "CityId", "dbo.Cities");
            DropForeignKey("dbo.FactWeathers", "CityId", "dbo.Cities");
            DropIndex("dbo.ForecastWeathers", new[] { "CityId" });
            DropIndex("dbo.FactWeathers", new[] { "CityId" });
            DropColumn("dbo.ForecastWeathers", "CityId");
            DropColumn("dbo.FactWeathers", "CityId");
            DropTable("dbo.Cities");
        }
    }
}
