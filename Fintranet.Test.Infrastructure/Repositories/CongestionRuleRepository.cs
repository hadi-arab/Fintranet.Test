using Fintranet.Test.Application.Abstractions.Repositories;
using Fintranet.Test.Domain.Entities;
using Fintranet.Test.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fintranet.Test.Infrastructure.Repositories
{
    public class CongestionRuleRepository : ICongestionRuleRepository
    {
        ApplicationDBContext _context;
        public CongestionRuleRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public void AddCongestionRule(CongestionRule congestionRule)
        {
            _context.CongestionRules.Add(congestionRule);
            _context.SaveChanges();
        }

        public void AddRangeCongestionRule(List<CongestionRule> congestionRules)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var congestionRule in congestionRules)
                {
                    var congestionRuleRes = _context.CongestionRules.FirstOrDefault(it => it.Key == congestionRule.Key);
                    if (congestionRuleRes == null)
                    {
                        _context.CongestionRules.Add(congestionRule);
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

        public IEnumerable<CongestionRule> GetAll()
        {
            return _context.CongestionRules.AsEnumerable();
        }
    }
}
