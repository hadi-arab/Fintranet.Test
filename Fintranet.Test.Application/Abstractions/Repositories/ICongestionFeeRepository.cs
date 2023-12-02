using Fintranet.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fintranet.Test.Application.Abstractions.Repositories
{
    public interface ICongestionFeeRepository
    {
        IEnumerable<CongestionFee> GetListByYear(int year);

        void AddRangeCongestionFee(IEnumerable<CongestionFee> congestionFees);
    }
}
