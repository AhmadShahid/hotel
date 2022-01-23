using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFynd.Domain.Entities
{
    public class HotelRate
    {
        public int Adults { get; set; }
        public int Los { get; set; }
        public Price Price { get; set; }
        public string RateDescription { get; set; }
        public string RateId { get; set; }
        public string RateName { get; set; }
        public IEnumerable<Rate> RateTags { get; set; }
        public DateTime TargetDay { get; set; }
    }
}
