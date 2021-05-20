namespace ECLab.Model.Data
{
    public record Product
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public decimal Price { get; init; }
    }
}