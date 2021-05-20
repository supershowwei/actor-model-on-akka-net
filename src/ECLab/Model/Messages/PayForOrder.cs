namespace ECLab.Model.Messages
{
    public sealed record PayForOrder
    {
        public int OrderId { get; init; }
    }
}