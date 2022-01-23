using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFynd.Application.Contracts;
using UFynd.Application.Features.Hotel.Dtos;
using UFynd.Domain.Entities;

namespace UFynd.Application.Feature.Hotel.Queries
{
    public class GetHotelRatesQueryHandler : IRequestHandler<GetHotelRatesQuery, HotelWithRatesDto>
    {
        private readonly IHotelRateRepository _hotelRateRepository;
        private readonly IMapper _mapper;
        public GetHotelRatesQueryHandler(IHotelRateRepository hotelRateRepository, IMapper mapper)
        {
            _hotelRateRepository = hotelRateRepository;
            _mapper = mapper;
        }
        public async Task<HotelWithRatesDto> Handle(GetHotelRatesQuery request, CancellationToken cancellationToken)
        {
            var hotelWithRates = await _hotelRateRepository.GetHotelsWithRates(request.HotelId, request.ArrivalDate);
            return _mapper.Map<HotelWithRatesDto>(hotelWithRates);
        }
    }
}
