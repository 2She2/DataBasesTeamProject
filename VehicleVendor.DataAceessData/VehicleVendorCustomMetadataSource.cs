namespace VehicleVendor.DataAceessData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Telerik.OpenAccess;
    using Telerik.OpenAccess.Metadata;
    using Telerik.OpenAccess.Metadata.Fluent;

    public class VehicleVendorCustomMetadataSource : FluentMetadataSource
    {
        public MappingConfiguration<DataAccessIncome> GetDataAccessIncomeMappingConfiguration()
        {
            MappingConfiguration<DataAccessIncome> configuration = this.GetDataAccessIncomeClassConfiguration();
            this.PrepareDataAccessIncomePropertyConfigurations(configuration);
            this.PrepareDataAccessIncomeAssociationConfigurations(configuration);

            return configuration;
        }

        public MappingConfiguration<DataAccessIncome> GetDataAccessIncomeClassConfiguration()
        {
            MappingConfiguration<DataAccessIncome> configuration = new MappingConfiguration<DataAccessIncome>();
            configuration.MapType(x => new { })
                         .WithConcurencyControl(OptimisticConcurrencyControlStrategy.Changed)
                         .ToTable("incomes");

            return configuration;
        }

        public void PrepareDataAccessIncomePropertyConfigurations(MappingConfiguration<DataAccessIncome> configuration)
        {
            configuration.HasProperty(x => x.Id)
                         .IsIdentity(KeyGenerator.Autoinc)
                         .HasFieldName("id")
                         .WithDataAccessKind(DataAccessKind.ReadWrite)
                         .ToColumn("Id")
                         .IsNotNullable()
                         .HasColumnType("integer")
                         .HasPrecision(0)
                         .HasScale(0);
            configuration.HasProperty(x => x.DealerId)
                         .HasFieldName("dealerId")
                         .WithDataAccessKind(DataAccessKind.ReadWrite)
                         .ToColumn("DealerId")
                         .IsNotNullable()
                         .HasColumnType("integer")
                         .HasPrecision(0)
                         .HasScale(0);
            configuration.HasProperty(x => x.Date)
                         .HasFieldName("date")
                         .WithDataAccessKind(DataAccessKind.ReadWrite)
                         .ToColumn("Date")
                         .IsNotNullable()
                         .HasColumnType("datetime");
            configuration.HasProperty(x => x.Amount)
                         .HasFieldName("amount")
                         .WithDataAccessKind(DataAccessKind.ReadWrite)
                         .ToColumn("Amount")
                         .IsNotNullable()
                         .HasColumnType("decimal")
                         .HasPrecision(18)
                         .HasScale(2);
        }

        public void PrepareDataAccessIncomeAssociationConfigurations(MappingConfiguration<DataAccessIncome> configuration)
        {
            configuration.HasAssociation(x => x.DataAccessDealer)
                         .HasFieldName("dataAccessDealer")
                         .WithOpposite(x => x.DataAccessIncomes)
                         .ToColumn("DealerId")
                         .HasConstraint((x, y) => x.DealerId == y.Id)
                         .IsRequired()
                         .WithDataAccessKind(DataAccessKind.ReadWrite);
        }

        public MappingConfiguration<DataAccessDealer> GetDataAccessDealerMappingConfiguration()
        {
            MappingConfiguration<DataAccessDealer> configuration = this.GetDataAccessDealerClassConfiguration();
            this.PrepareDataAccessDealerPropertyConfigurations(configuration);
            this.PrepareDataAccessDealerAssociationConfigurations(configuration);

            return configuration;
        }

        public MappingConfiguration<DataAccessDealer> GetDataAccessDealerClassConfiguration()
        {
            MappingConfiguration<DataAccessDealer> configuration = new MappingConfiguration<DataAccessDealer>();
            configuration.MapType(x => new { })
                         .WithConcurencyControl(OptimisticConcurrencyControlStrategy.Changed)
                         .ToTable("dealers");

            return configuration;
        }

        public void PrepareDataAccessDealerPropertyConfigurations(MappingConfiguration<DataAccessDealer> configuration)
        {
            configuration.HasProperty(x => x.Id)
                         .IsIdentity(KeyGenerator.Autoinc)
                         .HasFieldName("id")
                         .WithDataAccessKind(DataAccessKind.ReadWrite)
                         .ToColumn("Id")
                         .IsNotNullable()
                         .HasColumnType("integer")
                         .HasPrecision(0)
                         .HasScale(0);
            configuration.HasProperty(x => x.Company)
                         .HasFieldName("company")
                         .WithDataAccessKind(DataAccessKind.ReadWrite)
                         .ToColumn("Company")
                         .IsNullable()
                         .HasColumnType("longtext")
                         .HasLength(0);
            configuration.HasProperty(x => x.Address)
                         .HasFieldName("address")
                         .WithDataAccessKind(DataAccessKind.ReadWrite)
                         .ToColumn("Address")
                         .IsNullable()
                         .HasColumnType("longtext")
                         .HasLength(0);
            configuration.HasProperty(x => x.CountryId)
                         .HasFieldName("countryId")
                         .WithDataAccessKind(DataAccessKind.ReadWrite)
                         .ToColumn("CountryId")
                         .IsNotNullable()
                         .HasColumnType("integer")
                         .HasPrecision(0)
                         .HasScale(0);
        }

        public void PrepareDataAccessDealerAssociationConfigurations(MappingConfiguration<DataAccessDealer> configuration)
        {
            configuration.HasAssociation(x => x.DataAccessIncomes)
                         .HasFieldName("dataAccessIncomes")
                         .WithOpposite(x => x.DataAccessDealer)
                         .ToColumn("DealerId")
                         .HasConstraint((y, x) => x.DealerId == y.Id)
                         .WithDataAccessKind(DataAccessKind.ReadWrite);
            configuration.HasAssociation(x => x.DataAccessCountry)
                         .HasFieldName("dataAccessCountry")
                         .WithOpposite(x => x.DataAccessDealers)
                         .ToColumn("CountryId")
                         .HasConstraint((x, y) => x.CountryId == y.Id)
                         .IsRequired()
                         .WithDataAccessKind(DataAccessKind.ReadWrite);
        }

        public MappingConfiguration<DataAccessCountry> GetDataAccessCountryMappingConfiguration()
        {
            MappingConfiguration<DataAccessCountry> configuration = this.GetDataAccessCountryClassConfiguration();
            this.PrepareDataAccessCountryPropertyConfigurations(configuration);
            this.PrepareDataAccessCountryAssociationConfigurations(configuration);

            return configuration;
        }

        public MappingConfiguration<DataAccessCountry> GetDataAccessCountryClassConfiguration()
        {
            MappingConfiguration<DataAccessCountry> configuration = new MappingConfiguration<DataAccessCountry>();
            configuration.MapType(x => new { })
                         .WithConcurencyControl(OptimisticConcurrencyControlStrategy.Changed)
                         .ToTable("countries");

            return configuration;
        }

        public void PrepareDataAccessCountryPropertyConfigurations(MappingConfiguration<DataAccessCountry> configuration)
        {
            configuration.HasProperty(x => x.Id)
                         .IsIdentity(KeyGenerator.Autoinc)
                         .HasFieldName("id")
                         .WithDataAccessKind(DataAccessKind.ReadWrite)
                         .ToColumn("Id")
                         .IsNotNullable()
                         .HasColumnType("integer")
                         .HasPrecision(0)
                         .HasScale(0);
            configuration.HasProperty(x => x.Name)
                         .HasFieldName("name")
                         .WithDataAccessKind(DataAccessKind.ReadWrite)
                         .ToColumn("Name")
                         .IsNullable()
                         .HasColumnType("longtext")
                         .HasLength(0);
            configuration.HasProperty(x => x.Region)
                         .HasFieldName("region")
                         .WithDataAccessKind(DataAccessKind.ReadWrite)
                         .ToColumn("Region")
                         .IsNotNullable()
                         .HasColumnType("integer")
                         .HasPrecision(0)
                         .HasScale(0);
        }

        public void PrepareDataAccessCountryAssociationConfigurations(MappingConfiguration<DataAccessCountry> configuration)
        {
            configuration.HasAssociation(x => x.DataAccessDealers)
                         .HasFieldName("dataAccessDealers")
                         .WithOpposite(x => x.DataAccessCountry)
                         .ToColumn("CountryId")
                         .HasConstraint((y, x) => x.CountryId == y.Id)
                         .WithDataAccessKind(DataAccessKind.ReadWrite);
        }

        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> mappingConfigurations = new List<MappingConfiguration>();

            MappingConfiguration<DataAccessIncome> dataaccessincomeConfiguration = this.GetDataAccessIncomeMappingConfiguration();
            mappingConfigurations.Add(dataaccessincomeConfiguration);

            MappingConfiguration<DataAccessDealer> dataaccessdealerConfiguration = this.GetDataAccessDealerMappingConfiguration();
            mappingConfigurations.Add(dataaccessdealerConfiguration);

            MappingConfiguration<DataAccessCountry> dataaccesscountryConfiguration = this.GetDataAccessCountryMappingConfiguration();
            mappingConfigurations.Add(dataaccesscountryConfiguration);

            return mappingConfigurations;
        }

        protected override void SetContainerSettings(MetadataContainer container)
        {
            container.Name = "VehicleVendorMySqlDbContext";
            container.DefaultNamespace = "VehicleVendor.DataAceessData";
            container.NameGenerator.SourceStrategy = Telerik.OpenAccess.Metadata.NamingSourceStrategy.Property;
            container.NameGenerator.RemoveCamelCase = false;
        }
    }
}