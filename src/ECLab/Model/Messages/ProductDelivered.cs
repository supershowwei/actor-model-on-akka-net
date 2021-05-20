using System.Collections.Generic;
using ECLab.Model.Data;

namespace ECLab.Model.Messages
{
    public sealed record ProductDelivered
    {
        public List<Product> Products { get; init; }
    }
}