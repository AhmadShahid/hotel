using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UFynd.Application.Contracts;
using UFynd.Application.Exceptions;
using UFynd.Common.Constants;
using UFynd.Domain.Entities;

namespace UFynd.Infrastructure.Repositories
{
    public class HotelRepository : IHotelRateRepository
    {
        public readonly IDataReader<HotelSummary> _dataReader;
        public HotelRepository(IDataReader<HotelSummary> jsonDataReader)
        {
            _dataReader = jsonDataReader;
        }
        public async Task<HotelSummary> GetHotelsWithRates(int hotelId, DateTime? arrivalDate)
        {
            try
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UFyndAppConstant.HotelRateJsonFile);
                var hotelSummaries = await _dataReader.LoadAsync(filePath);

                var hotel = hotelSummaries
                .FirstOrDefault(x => x.Hotel.HotelID == hotelId);

                if (hotel == null)
                {
                    throw new NotFoundException(nameof(Hotel), hotelId);
                }
                if (arrivalDate != null)
                {
                    hotel.HotelRates = hotel.HotelRates.Where(hr => hr.TargetDay.Date == Convert.ToDateTime(arrivalDate).Date);
                }
                return hotel;
            }
            catch (NotFoundException)
            {
                throw new NotFoundException(nameof(Hotel), hotelId);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ErrorMessage.UnExpectedErrorOccurred, ex);
            }

        }
    }
}
