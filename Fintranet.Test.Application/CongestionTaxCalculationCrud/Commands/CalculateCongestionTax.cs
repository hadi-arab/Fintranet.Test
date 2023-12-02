using Fintranet.Test.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fintranet.Test.Application.CongestionTaxCalculationCrud.Commands
{
    public class CalculateCongestionTax : IRequest<CongestionTaxCalculation>
    {
        public int? CarType { get; set; }
        public DateTime?[] Dates { get; set; }
    }
}
