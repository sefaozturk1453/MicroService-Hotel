using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class HotelInformationMessage
    {
        public Guid HotelId { get; set; }
        public byte Type { get; set; }
        public string Info { get; set; }
    }
}
