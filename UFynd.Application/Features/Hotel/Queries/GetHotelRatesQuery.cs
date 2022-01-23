using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFynd.Application.Features.Hotel.Dtos;
using UFynd.Domain.Entities;

namespace UFynd.Application.Feature.Hotel.Queries
{
    public class GetHotelRatesQuery : IRequest<HotelWithRatesDto>
    {
        public int HotelId { get; set; }
        public DateTime? ArrivalDate { get; set; }
    }
}
