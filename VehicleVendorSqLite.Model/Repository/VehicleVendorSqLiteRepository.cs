namespace VehicleVendorSqLite.Model.Repository
{
    using System.Data.Entity;
    using System.Linq;

    public class VehicleVendorSqLiteRepository : IVehicleVendorSqLiteRepository
    {
        private SqLiteContext context;

        public VehicleVendorSqLiteRepository(SqLiteContext context)
        {
            this.context = context;
        }

        public DbSet<DealersCosts> DealersCostsSet 
        {
            get
            {
                return this.context.DealersCostsSet;
            }
        }
    }
}
