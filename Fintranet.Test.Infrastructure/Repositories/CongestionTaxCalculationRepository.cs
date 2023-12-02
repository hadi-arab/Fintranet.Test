using Fintranet.Test.Application.Abstractions.Repositories;
using Fintranet.Test.Domain.Entities;
using Fintranet.Test.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fintranet.Test.Infrastructure.Repositories
{
    public class CongestionTaxCalculationRepository : ICongestionTaxCalculationRepository
    {
        ApplicationDBContext _context;

        public CongestionTaxCalculationRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task AddCongestionTaxCalculation(CongestionTaxCalculation congestionTaxCalculation)
        {
            await _context.CongestionTaxCalculations.AddAsync(congestionTaxCalculation);
            await _context.SaveChangesAsync();
        }
    }
}
