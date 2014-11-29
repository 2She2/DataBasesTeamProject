namespace VehicleVendor.Models
{
    using System;

    public class SaleDetails
    {
        public SaleDetails()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid SaleId { get; set; }

        public int Quantity { get; set; }

        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual Sale Sale { get; set; }
    }
}
