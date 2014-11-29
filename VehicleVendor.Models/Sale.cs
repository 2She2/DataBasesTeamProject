namespace VehicleVendor.Models
{
    using System;
    using System.Collections.Generic;

    public class Sale
    {
        private ICollection<SaleDetails> saleDetails;

        public Sale()
        {
            this.Id = Guid.NewGuid();
            this.saleDetails = new HashSet<SaleDetails>();
        }

        public Guid Id { get; set; }

        public int DealerId { get; set; }

        public DateTime SaleDate { get; set; }

        public virtual ICollection<SaleDetails> SaleItems
        {
            get 
            {
                return this.saleDetails;
            }

            set 
            {
                this.saleDetails = value; 
            }
        }

        public virtual Dealer Dealer { get; set; }
    }
}
