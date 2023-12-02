using Fintranet.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fintranet.Test.Application.Abstractions.Repositories
{
    public interface ICongestionRuleRepository
    {
        void AddCongestionRule(CongestionRule congestionRule);

        void AddRangeCongestionRule(List<CongestionRule> congestionRules);

        IEnumerable<CongestionRule> GetAll();
    }
}
