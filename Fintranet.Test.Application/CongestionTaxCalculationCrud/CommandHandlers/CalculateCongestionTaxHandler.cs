using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Fintranet.Test.Application.CongestionTaxCalculationCrud.Commands;
using Fintranet.Test.Application.Abstractions.Repositories;
using Fintranet.Test.Domain.Entities;
using AutoMapper;
using Fintranet.Test.Application.Tools.Calculator;
using Fintranet.Test.Application.Tools;
using System.Linq;

namespace Fintranet.Test.Application.CongestionTaxCalculationCrud.CommandHandlers
{
    public class CalculateCongestionTaxHandler : IRequestHandler<CalculateCongestionTax, CongestionTaxCalculation>
    {
        private readonly ICongestionTaxCalculationRepository _congestionTaxCalculationRepository;
        private readonly ICongestionFeeRepository _congestionFeeRepository;
        private readonly ICongestionRuleRepository _congestionRuleRepository;
        private readonly ITollFreeVehicleRepository _tollFreeVehicleRepository;
        IMapper _mapper;

        public CalculateCongestionTaxHandler(ICongestionTaxCalculationRepository congestionTaxCalculationRepository,
            ICongestionFeeRepository congestionFeeRepository,
            ICongestionRuleRepository congestionRuleRepository,
            ITollFreeVehicleRepository tollFreeVehicleRepository,
            IMapper mapper)
        {
            _congestionTaxCalculationRepository = congestionTaxCalculationRepository;
            _congestionFeeRepository = congestionFeeRepository;
            _congestionRuleRepository = congestionRuleRepository;
            _tollFreeVehicleRepository = tollFreeVehicleRepository;
            _mapper = mapper;
        }

        public async Task<CongestionTaxCalculation> Handle(CalculateCongestionTax request, CancellationToken cancellationToken)
        {
            var congestionTaxCalculation = _mapper.Map<CongestionTaxCalculation>(request);
            congestionTaxCalculation.RegisterDate = DateTime.UtcNow;

            // get all rules
            var congRules = _congestionRuleRepository.GetAll();
            var yearLimit = int.Parse(congRules.First(it => it.Key == "CongestionCalculationYearLimit").Value);
            var tollingMinutes = int.Parse(congRules.First(it => it.Key == "SeveralTollingStationsPeriodInMinutes").Value);
            var maxTaxFee = int.Parse(congRules.First(it => it.Key == "MaxCongestionTaxLimitInSEK").Value);

            // List of fees
            var congFee = _congestionFeeRepository.GetListByYear(yearLimit).ToList();

            // check if vehicle is toll free
            var tollfree = _tollFreeVehicleRepository.IsVehicleTollFree(request.CarType ?? 0);

            // tax calculator options
            CongestionTaxCalculatorOptions options = new CongestionTaxCalculatorOptions();
            options.CongestionFeeRule = CongestionFeeRule.ConvertCongestionFeeRule(congFee);
            options.CongestionYearLimit = yearLimit;
            options.SeveralTollingStationsLimitInMinutes = tollingMinutes;
            options.MaxCongestionTaxLimitForOneDay = maxTaxFee;
            options.Dates = request.Dates;
            options.IsVehicleTollFree = tollfree;

            // initialize tax calculator
            CongestionTaxCalculator congestionTaxCalculator = new CongestionTaxCalculator(options);
            congestionTaxCalculation.CalculatedTax = congestionTaxCalculator.GetTax();

            await _congestionTaxCalculationRepository.AddCongestionTaxCalculation(congestionTaxCalculation);
            return congestionTaxCalculation;
        }
    }
}
