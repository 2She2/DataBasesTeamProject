namespace VehicleVendorSqLite.Model
{
    using System.Data.Entity;
    using System.Linq;
    using VehicleVendor.Models;

    public interface IVehicleVendorSqLiteRepository
    {
        DbSet<DealersCosts> DealersCostsSet { get; }
    }
}
