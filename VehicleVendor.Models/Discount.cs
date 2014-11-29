namespace VehicleVendor.Models
{
    public class Discount
    {
        public int Id { get; set; }

        public double Amount { get; set; }

        public int DealerId { get; set; }

        public virtual Dealer Dealer { get; set; }
    }
}
