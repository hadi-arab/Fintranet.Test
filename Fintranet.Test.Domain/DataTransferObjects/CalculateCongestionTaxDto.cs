using System;
using System.Collections.Generic;
using System.Text;

namespace Fintranet.Test.Domain.DataTransferObjects
{
    public record CalculateCongestionTaxDto
    {
        public int? CarType { get; init; }
        public DateTime?[] Dates { get; init; }
    }
}
