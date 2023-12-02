using AutoMapper;
using Fintranet.Test.Application.CongestionTaxCalculationCrud.Commands;
using Fintranet.Test.Domain.DataTransferObjects;
using Fintranet.Test.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Fintranet.Test.Application.Common.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<CalculateCongestionTaxDto, CalculateCongestionTax>()
                .ForMember(d => d.Dates,
                    opt => opt.MapFrom(s => s.Dates.ToList().OrderBy(it => it).ToArray())
                );

            CreateMap<CalculateCongestionTax, CongestionTaxCalculation>()
                .ForMember(d => d.RequestParams,
                    opt => opt.MapFrom(s => JsonConvert.SerializeObject(s))
                );

            CreateMap<CongestionTaxCalculation, CongestionTaxCalculationDto>()
                .ForMember(d => d.CalculatedTax,
                    opt => opt.MapFrom(s => s.CalculatedTax)
                )
                .ForMember(d => d.CarType,
                    opt => opt.MapFrom(s => s.CarType)
                );
        }
    }
}
