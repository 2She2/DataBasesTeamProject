namespace VehicleVendor.DataAceessData
{
    using System;
    using System.Linq;
    using Telerik.OpenAccess;

    public class DatabaseInitializer
    {
        private IVehicleVendorMySqlDbContextUnitOfWork context;

        public DatabaseInitializer(IVehicleVendorMySqlDbContextUnitOfWork context)
        {
            this.context = context;
        }

        public void UpdateDatabase()
        {
                var schemaHandler = this.context.GetSchemaHandler();
                EnsureDB(schemaHandler);
        }

        private void EnsureDB(ISchemaHandler schemaHandler)
        {
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }
    }
}
