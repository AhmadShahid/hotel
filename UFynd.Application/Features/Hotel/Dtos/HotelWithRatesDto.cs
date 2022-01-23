using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFynd.Application.Mappings;
using UFynd.Domain.Entities;

namespace UFynd.Application.Features.Hotel.Dtos
{
    public class HotelWithRatesDto : IMapFrom<HotelSummary>
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public int Classification { get; set; }
        public double Reviewscore { get; set; }

        public List<HotelRateDto> HotelRates { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HotelSummary, HotelWithRatesDto>()
                .ForMember(d => d.HotelId, opt => opt.MapFrom(s => s.Hotel.HotelID))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Hotel.Name))
                .ForMember(d => d.Classification, opt => opt.MapFrom(s => s.Hotel.Classification))
                .ForMember(d => d.Reviewscore, opt => opt.MapFrom(s => s.Hotel.Reviewscore))
                .ForMember(d => d.HotelRates, opts => opts.MapFrom(s => s.HotelRates));
        }
    }
}
