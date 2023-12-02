using Fintranet.Test.Application.Abstractions.Repositories;
using Fintranet.Test.Domain.Entities;
using Fintranet.Test.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fintranet.Test.Infrastructure.Repositories
{
    public class CongestionFeeRepository : ICongestionFeeRepository
    {
        ApplicationDBContext _context;

        public CongestionFeeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public void AddRangeCongestionFee(IEnumerable<CongestionFee> congestionFees)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var congestionFee in congestionFees)
                {
                    var congestionFeeRes = _context.CongestionFees.FirstOrDefault(
                        it => it.StartTime == congestionFee.StartTime
                        && it.EndTime == congestionFee.EndTime);

                    if (congestionFeeRes == null)
                    {
                        _context.CongestionFees.Add(congestionFee);
                        _context.SaveChanges();
                    }
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public IEnumerable<CongestionFee> GetListByYear(int year)
        {
            return _context.CongestionFees.Where(it =>  it.Year == year);
        }
    }
}
