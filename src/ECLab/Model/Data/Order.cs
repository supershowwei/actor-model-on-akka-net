using System.Collections.Generic;

namespace ECLab.Model.Data
{
    public record Order
    {
        public int Id { get; init; }

        public List<Product> Products { get; init; }

        public decimal TotalPrice { get; init; }
    }
}