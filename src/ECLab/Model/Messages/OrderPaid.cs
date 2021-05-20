using ECLab.Model.Data;

namespace ECLab.Model.Messages
{
    public sealed record OrderPaid
    {
        public Order Order { get; init; }
    }
}