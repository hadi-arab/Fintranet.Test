using System;
using System.Collections.Generic;
using System.Text;

namespace Fintranet.Test.Domain.DataTransferObjects
{
    public record CongestionTaxCalculationDto
    {
        public int CalculatedTax { get; init; }
        public int CarType { get; init; }
    }
}
