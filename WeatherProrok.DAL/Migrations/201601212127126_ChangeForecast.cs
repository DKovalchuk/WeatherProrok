namespace WeatherProrok.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeForecast : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ForecastWeathers", "ThreeHourTemp", c => c.Int(nullable: false));
            AddColumn("dbo.ForecastWeathers", "SixHourTemp", c => c.Int(nullable: false));
            AddColumn("dbo.ForecastWeathers", "NineHourTemp", c => c.Int(nullable: false));
            AddColumn("dbo.ForecastWeathers", "ElevenHourTemp", c => c.Int(nullable: false));
            DropColumn("dbo.ForecastWeathers", "Temp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ForecastWeathers", "Temp", c => c.Int(nullable: false));
            DropColumn("dbo.ForecastWeathers", "ElevenHourTemp");
            DropColumn("dbo.ForecastWeathers", "NineHourTemp");
            DropColumn("dbo.ForecastWeathers", "SixHourTemp");
            DropColumn("dbo.ForecastWeathers", "ThreeHourTemp");
        }
    }
}
