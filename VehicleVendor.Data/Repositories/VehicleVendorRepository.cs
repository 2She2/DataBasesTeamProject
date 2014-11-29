namespace VehicleVendor.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using VehicleVendor.Data;
    using VehicleVendor.Models;

    public class VehicleVendorRepository : IVehicleVendorRepository
    {
        private IEnumerable<IVehicleVendorDbContext> contexts;

        public VehicleVendorRepository(IEnumerable<IVehicleVendorDbContext> contexts)
        {
            this.contexts = contexts;
        }

        public DbSet<Dealer> Dealers 
        {
            get
            {
                return this.contexts.FirstOrDefault().Dealers;
            }
        }

        public DbSet<Vehicle> Vehicles
        {
            get
            {
                return this.contexts.FirstOrDefault().Vehicles;
            }
        }

        public DbSet<Country> Countries
        {
            get
            {
                return this.contexts.FirstOrDefault().Countries;
            }
        }

        public DbSet<Sale> Sales
        {
            get
            {
                return this.contexts.FirstOrDefault().Sales;
            }
        }

        public DbSet<SaleDetails> SalesDetails
        {
            get
            {
                return this.contexts.FirstOrDefault().SalesDetails;
            }
        }

        public DbSet<Discount> Discounts
        {
            get
            {
                return this.contexts.FirstOrDefault().Discounts;
            }
        }

        public void Add<T>(T entity) where T : class
        {
            foreach (var context in this.contexts)
            {
                var entry = this.AttachIfDetached(entity, context);
                entry.State = EntityState.Added;
            }
        }

        public void Update<T>(T old, T entity) where T : class
        {
            this.Detach<T>(old);
            foreach (var context in this.contexts)
            {
                var entry = this.AttachIfDetached<T>(entity, context);
                entry.State = EntityState.Modified;
            }
        }

        public void Delete<T>(T entity) where T : class
        {
            foreach (var context in this.contexts)
            {
                var entry = this.AttachIfDetached<T>(entity, context);
                entry.State = EntityState.Deleted;
            }
        }

        public void SaveChanges()
        {
            foreach (var context in this.contexts)
            {
                context.SaveChanges();
            }
        }

        public void Detach<T>(T entity) where T : class
        {
            foreach (var context in this.contexts)
            {
                var entry = context.Entry(entity);
                entry.State = EntityState.Detached;
            }
        }

        private DbEntityEntry AttachIfDetached<T>(T entity, IVehicleVendorDbContext context) where T : class
        {
            var entry = context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                context.Set<T>().Attach(entity);
            }

            return entry;
        }
    }
}
