namespace VehicleVendor.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }

        public decimal Price { get; set; }
    }
}
