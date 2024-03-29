#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ContextGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using VehicleVendor.DataAceessData;

namespace VehicleVendor.DataAceessData	
{
	public partial class VehicleVendorMySqlDbContext : OpenAccessContext, IVehicleVendorMySqlDbContextUnitOfWork
	{
        private static string connectionStringName = @"NissanConnection";
			
		private static BackendConfiguration backend = GetBackendConfiguration();

        private static MetadataSource metadataSource = new VehicleVendorMySqlDbContextMetadataSource();
		
		public VehicleVendorMySqlDbContext()
			:base(connectionStringName, backend, metadataSource)
		{ }
		
		public VehicleVendorMySqlDbContext(string connection)
			:base(connection, backend, metadataSource)
		{ }
		
		public VehicleVendorMySqlDbContext(BackendConfiguration backendConfiguration)
			:base(connectionStringName, backendConfiguration, metadataSource)
		{ }
			
		public VehicleVendorMySqlDbContext(string connection, MetadataSource metadataSource)
			:base(connection, backend, metadataSource)
		{ }

        public VehicleVendorMySqlDbContext(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource)
			:base(connection, backendConfiguration, metadataSource)
		{ }

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
			
		public static BackendConfiguration GetBackendConfiguration()
		{
			BackendConfiguration backend = new BackendConfiguration();
			backend.Backend = "MySql";
			backend.ProviderName = "MySql.Data.MySqlClient";
		
			CustomizeBackendConfiguration(ref backend);
		
			return backend;
		}
		
		/// <summary>
		/// Allows you to customize the BackendConfiguration of VehicleVendorMySqlDbContext.
		/// </summary>
		/// <param name="config">The BackendConfiguration of VehicleVendorMySqlDbContext.</param>
		static partial void CustomizeBackendConfiguration(ref BackendConfiguration config);
		
	}
	
	public interface IVehicleVendorMySqlDbContextUnitOfWork : IUnitOfWork
	{
        ISchemaHandler GetSchemaHandler();

        IQueryable<DataAccessIncome> DataAccessIncomes { get; }

        IQueryable<DataAccessDealer> DataAccessDealers { get; }

        IQueryable<DataAccessCountry> DataAccessCountries { get; }
	}
}
#pragma warning restore 1591
