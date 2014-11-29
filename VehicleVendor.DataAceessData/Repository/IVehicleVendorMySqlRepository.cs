namespace VehicleVendor.DataAceessData.Repository
{
    using System.Linq;
    using Telerik.OpenAccess;
    using VehicleVendor.DataAceessData;

    public interface IVehicleVendorMySqlRepository
    {
        IQueryable<DataAccessIncome> DataAccessIncomes { get; }

        IQueryable<DataAccessDealer> DataAccessDealers { get; }

        IQueryable<DataAccessCountry> DataAccessCountries { get; }

        void Add<T>(T entity) where T : class;

        void Update<T>(T old, T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        void SaveChanges();
    }
}
