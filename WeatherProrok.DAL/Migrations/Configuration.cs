namespace WeatherProrok.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WeatherProrok.DAL.WeatherContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WeatherProrok.DAL.WeatherContext context)
        {
            context.CloudityDict.AddOrUpdate(
                c => c.Name,
                new Model.Cloudity { Id = Guid.NewGuid(), Name = "����" },
                new Model.Cloudity { Id = Guid.NewGuid(), Name = "�����������" },
                new Model.Cloudity { Id = Guid.NewGuid(), Name = "�������" },
                new Model.Cloudity { Id = Guid.NewGuid(), Name = "��������" },
                new Model.Cloudity { Id = Guid.NewGuid(), Name = "����������" }
                );

            context.PrecipitationDict.AddOrUpdate(
                p => p.Name,
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "��� ������" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "������ �����" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "�����" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "������� �����" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "������ ����" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "����" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "������� ����" },
                new Model.Precipitation { Id = Guid.NewGuid(), Name = "������" }
                );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
