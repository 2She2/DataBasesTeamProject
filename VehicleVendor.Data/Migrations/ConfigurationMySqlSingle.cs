namespace VehicleVendor.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MySql.Data.Entity;

    public sealed class ConfigurationMySql : DbMigrationsConfiguration<VehicleVendorDbContextInMySql>
    {
        public ConfigurationMySql() :
            base()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
            this.SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
        }
    }
}
