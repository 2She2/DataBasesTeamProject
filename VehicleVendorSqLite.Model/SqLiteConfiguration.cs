namespace VehicleVendorSqLite.Model
{
    using System.Data.Entity.Migrations;
    using VehicleVendor.Data;

    public sealed class SqLiteConfiguration : DbMigrationsConfiguration<SqLiteContext>
    {
        public SqLiteConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
        }
    }
}
