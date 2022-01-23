using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFynd.Domain.Entities;

namespace UFynd.Application.Contracts
{
    public interface IHotelRateRepository
    {
         Task<HotelSummary> GetHotelsWithRates(int hotelId, DateTime? arrivalDate);
    }
}
