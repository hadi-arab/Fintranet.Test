using Fintranet.Test.Application.CongestionTaxCalculationCrud.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fintranet.Test.Application.Validators
{
    public class CalculateCongestionTaxValidator: AbstractValidator<CalculateCongestionTax>
    {
        public CalculateCongestionTaxValidator()
        {
            RuleFor(calculateCongestionTax => calculateCongestionTax.CarType)
                .NotNull().WithName("Car Type");

            RuleFor(calculateCongestionTax => calculateCongestionTax.Dates)
                .NotNull().Must(Any).WithName("Dates");
        }

        private bool Any(DateTime?[] dates)
        {
            if (dates.Length == 0)
            {
                return false;
            }

            foreach (var date in dates)
            {
                if (date != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
