using System.Collections.Generic;

namespace ECLab.Model.Messages
{
    public sealed record OrderProducts
    {
        public List<int> ProductIDs { get; init; }
    }
}