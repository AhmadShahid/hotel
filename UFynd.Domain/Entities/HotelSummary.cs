using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFynd.Domain.Entities
{
    public class HotelSummary
    {
        public Hotel Hotel { get; set; }
        public IEnumerable<HotelRate> HotelRates { get; set; }
    }
}
