namespace VehicleVendorSqLite.Model
{
    using System.Data.Entity;

    public class SqLiteContext : DbContext
    {
        public SqLiteContext()
            : base("nissan_costs")
        {
        }

        public DbSet<DealersCosts> DealersCostsSet { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
