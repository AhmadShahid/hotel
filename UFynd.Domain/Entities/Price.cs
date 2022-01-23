using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFynd.Domain.Entities
{
    public class Price
    {
        public string Currency { get; set; }
        public double NumericFloat { get; set; }
        public int NumericInteger { get; set; }
    }
}
