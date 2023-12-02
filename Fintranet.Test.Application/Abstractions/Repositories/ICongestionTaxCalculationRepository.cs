using Fintranet.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fintranet.Test.Application.Abstractions.Repositories
{
    public interface ICongestionTaxCalculationRepository
    {
        Task AddCongestionTaxCalculation(CongestionTaxCalculation congestionTaxCalculation);
    }
}
