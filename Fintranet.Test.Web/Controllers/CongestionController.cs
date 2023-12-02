using AutoMapper;
using Fintranet.Test.Application.CongestionTaxCalculationCrud.Commands;
using Fintranet.Test.Domain.DataTransferObjects;
using Fintranet.Test.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Fintranet.Test.Application.Tools;
using System.Linq;
using System.Text;

namespace Fintranet.Test.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongestionController : ControllerBase
    {
        ILogger<CongestionController> _logger;
        IMemoryCache _memoryCache;
        IMediator _mediator;
        IMapper _mapper;

        public CongestionController(ILogger<CongestionController> logger,
            IMemoryCache memoryCache,
            IMediator mediator,
            IMapper mapper)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Calculate Congestion Tax By Car Type For One Day and More Than One Day.
        /// </summary>
        [HttpPost]
        [Route("calculate-congestion-tax")]
        public async Task<IActionResult> CalculateCongestionTax([FromBody] CalculateCongestionTaxDto calculateCongestionTaxDto)
        {
            var calculateCongestionTax = _mapper.Map<CalculateCongestionTax>(calculateCongestionTaxDto);

            var key = HashCalculator.NewKey(calculateCongestionTax);
            var cacheResult = _memoryCache.Get(nameof(CalculateCongestionTax) + ":" + key);
            if (cacheResult != null)
            {
                var cacheDto = _mapper.Map<CongestionTaxCalculationDto>((CongestionTaxCalculation)cacheResult);
                return Ok(cacheDto);
            }

            var congestionTaxCalculation = await _mediator.Send(calculateCongestionTax);
            _memoryCache.Set(nameof(CalculateCongestionTax) + ":" + key, congestionTaxCalculation, new TimeSpan(0, 29, 0));

            var congestionTaxCalculationDto = _mapper.Map<CongestionTaxCalculationDto>(congestionTaxCalculation);
            return Ok(congestionTaxCalculationDto);
        }
    }
}
