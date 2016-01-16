namespace WeatherProrok.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFactWeather : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FactWeathers", "CloudityId", "dbo.Cloudities");
            DropForeignKey("dbo.FactWeathers", "PrecipitationId", "dbo.Precipitations");
            DropIndex("dbo.FactWeathers", new[] { "CloudityId" });
            DropIndex("dbo.FactWeathers", new[] { "PrecipitationId" });
            RenameColumn(table: "dbo.FactWeathers", name: "CloudityId", newName: "Cloudity_Id");
            RenameColumn(table: "dbo.FactWeathers", name: "PrecipitationId", newName: "Precipitation_Id");
            AddColumn("dbo.FactWeathers", "Cloudity", c => c.String());
            AddColumn("dbo.FactWeathers", "Precipitations", c => c.String());
            AlterColumn("dbo.FactWeathers", "Cloudity_Id", c => c.Guid());
            AlterColumn("dbo.FactWeathers", "Precipitation_Id", c => c.Guid());
            CreateIndex("dbo.FactWeathers", "Cloudity_Id");
            CreateIndex("dbo.FactWeathers", "Precipitation_Id");
            AddForeignKey("dbo.FactWeathers", "Cloudity_Id", "dbo.Cloudities", "Id");
            AddForeignKey("dbo.FactWeathers", "Precipitation_Id", "dbo.Precipitations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FactWeathers", "Precipitation_Id", "dbo.Precipitations");
            DropForeignKey("dbo.FactWeathers", "Cloudity_Id", "dbo.Cloudities");
            DropIndex("dbo.FactWeathers", new[] { "Precipitation_Id" });
            DropIndex("dbo.FactWeathers", new[] { "Cloudity_Id" });
            AlterColumn("dbo.FactWeathers", "Precipitation_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.FactWeathers", "Cloudity_Id", c => c.Guid(nullable: false));
            DropColumn("dbo.FactWeathers", "Precipitations");
            DropColumn("dbo.FactWeathers", "Cloudity");
            RenameColumn(table: "dbo.FactWeathers", name: "Precipitation_Id", newName: "PrecipitationId");
            RenameColumn(table: "dbo.FactWeathers", name: "Cloudity_Id", newName: "CloudityId");
            CreateIndex("dbo.FactWeathers", "PrecipitationId");
            CreateIndex("dbo.FactWeathers", "CloudityId");
            AddForeignKey("dbo.FactWeathers", "PrecipitationId", "dbo.Precipitations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FactWeathers", "CloudityId", "dbo.Cloudities", "Id", cascadeDelete: true);
        }
    }
}
