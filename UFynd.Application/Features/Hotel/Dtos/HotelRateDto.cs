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
    public class HotelRateDto: IMapFrom<HotelRate>
    {
        public int Adults { get; set; }
        public int LengthOfStay { get; set; }
        public string RateDescription { get; set; }
        public string RateId { get; set; }
        public string RateName { get; set; }
        public DateTime ArrivalDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HotelRate, HotelRateDto>()
                .ForMember(d => d.LengthOfStay, opt => opt.MapFrom(s => s.Los))
                .ForMember(d => d.RateId, opt => opt.MapFrom(s => s.RateId))
                .ForMember(d => d.ArrivalDate, opt => opt.MapFrom(s => s.TargetDay));

        }
    }
}
