namespace Towns.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Towns.EFContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Towns.EFContext context)
        {
            #region Regions
            context.Regions.AddOrUpdate(a => a.Id, new Entity.Region
            {
                Id = 1,
                Name = "Рівненська"
            });
            context.Regions.AddOrUpdate(a => a.Id, new Entity.Region
            {
                Id = 2,
                Name = "Волинська"
            });
            context.Regions.AddOrUpdate(a => a.Id, new Entity.Region
            {
                Id = 3,
                Name = "Львівська"
            });
            context.Regions.AddOrUpdate(a => a.Id, new Entity.Region
            {
                Id = 4,
                Name = "Івано-Франківська"
            });
            context.Regions.AddOrUpdate(a => a.Id, new Entity.Region
            {
                Id = 5,
                Name = "Закарпатська"
            });
            context.Regions.AddOrUpdate(a => a.Id, new Entity.Region
            {
                Id = 6,
                Name = "Чернівецька"
            });
            context.Regions.AddOrUpdate(a => a.Id, new Entity.Region
            {
                Id = 7,
                Name = "Хмельницька"
            });
            context.Regions.AddOrUpdate(a => a.Id, new Entity.Region
            {
                Id = 8,
                Name = "Тернопільська"
            });
            context.Regions.AddOrUpdate(a => a.Id, new Entity.Region
            {
                Id = 9,
                Name = "Житомирська"
            });
            context.Regions.AddOrUpdate(a => a.Id, new Entity.Region
            {
                Id = 10,
                Name = "Вінницька"
            });
            context.Regions.AddOrUpdate(a => a.Id, new Entity.Region
            {
                Id = 11,
                Name = "Київська"
            });
            #endregion

            #region TownTypes
            context.TownTypes.AddOrUpdate(a => a.Id, new Entity.TownType
            {
                Id = 1,
                Name = "Обласний центр"
            });
            context.TownTypes.AddOrUpdate(a => a.Id, new Entity.TownType
            {
                Id = 2,
                Name = "Місто"
            });
            context.TownTypes.AddOrUpdate(a => a.Id, new Entity.TownType
            {
                Id = 3,
                Name = "Село"
            });

            #endregion

            #region TownsRivnenska
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 1,
                Name = "Рівне",
                TownTypeId = 1,
                RegionId = 1
            });
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 2,
                Name = "Сарни",
                TownTypeId = 2,
                RegionId = 1
            });
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 3,
                Name = "Карпилівка",
                TownTypeId = 3,
                RegionId = 1
            });
            #endregion
            #region TownsVolynska
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 4,
                Name = "Луцьк",
                TownTypeId = 1,
                RegionId = 2
            });
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 5,
                Name = "Шацьк",
                TownTypeId = 3,
                RegionId = 2
            });
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 6,
                Name = "Ковель",
                TownTypeId = 2,
                RegionId = 2
            });
            #endregion
            #region TownsLvivska
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 7,
                Name = "Львів",
                TownTypeId = 1,
                RegionId = 3
            });
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 8,
                Name = "Радехів",
                TownTypeId = 2,
                RegionId = 3
            });
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 9,
                Name = "Сокаль",
                TownTypeId = 2,
                RegionId = 3
            });
            #endregion
            #region TownsFrankivska
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 10,
                Name = "Коломия",
                TownTypeId = 2,
                RegionId = 4
            });
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 11,
                Name = "Делятин",
                TownTypeId = 2,
                RegionId = 4
            });
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 12,
                Name = "Долина",
                TownTypeId = 2,
                RegionId = 4
            });
            #endregion
            #region TownsZakarpatska
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 13,
                Name = "Нижня Апша",
                TownTypeId = 3,
                RegionId = 5
            });
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 14,
                Name = "Ясіня",
                TownTypeId = 2,
                RegionId = 5
            });
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 15,
                Name = "Тячів",
                TownTypeId = 2,
                RegionId = 5
            });
            #endregion
            #region TownsChernivetska
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 16,
                Name = "Вижниця",
                TownTypeId = 2,
                RegionId = 6
            });
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 17,
                Name = "Чернівці",
                TownTypeId = 1,
                RegionId = 6
            });
            context.Towns.AddOrUpdate(a => a.Id, new Entity.Town
            {
                Id = 18,
                Name = "Сторожинець",
                TownTypeId = 2,
                RegionId = 6
            });

            #endregion
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
