namespace VehicleVendorSqLite.Model
{
    public class DealerCosts
    {
        public int Id { get; set; }

        public string Dealer { get; set; }

        public decimal ConstCosts { get; set; }

        public decimal SaleCosts { get; set; }
    }
}
