using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFynd.Domain.Entities
{
    public class Hotel
    {
        public int HotelID { get; set; }
        public int Classification { get; set; }
        public string Name { get; set; }
        public double Reviewscore { get; set; }
    }
}
