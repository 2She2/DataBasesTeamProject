namespace VehicleVendor.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VehicleVendor.Data;

    public sealed class Configuration : DbMigrationsConfiguration<VehicleVendorDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(VehicleVendorDbContext context)
        {
        }
    }
}
