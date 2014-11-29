namespace VehicleVendorSqLite.Model
{
    public class DealersCosts
    {
        public int Id { get; set; }

        public string Dealer { get; set; }

        public decimal ConstCosts { get; set; }

        public decimal SaleCosts { get; set; }
    }
}
