namespace VehicleVendor.DataAceessData
{
    using System;
    using System.Linq;
    using Telerik.OpenAccess;
    using Telerik.OpenAccess.Metadata;

    public partial class VehicleVendorMySqlDbContext : OpenAccessContext, IVehicleVendorMySqlDbContext
    {
        private static MetadataSource customMetadataSource = new VehicleVendorMySqlDbContextMetadataSource();

        public const string NissanConnectionStringName = @"NissanConnection";

        public static MetadataSource CustomMetadataSource
        {
            get
            {
                return customMetadataSource;
            }
        }

        public ISchemaHandler GetSchemaHandler()
        {
            return this.GetSchemaHandler();
        }

        public IQueryable<DataAccessIncome> DataAccessIncomes
        {
            get
            {
                return this.GetAll<DataAccessIncome>();
            }
        }

        public IQueryable<DataAccessDealer> DataAccessDealers
        {
            get
            {
                return this.GetAll<DataAccessDealer>();
            }
        }

        public IQueryable<DataAccessCountry> DataAccessCountries
        {
            get
            {
                return this.GetAll<DataAccessCountry>();
            }
        }
    }
}