using ECLab.Model.Data;

namespace ECLab.Model.Messages
{
    public sealed record OrderCreated
    {
        public Order Order { get; init; }
    }
}