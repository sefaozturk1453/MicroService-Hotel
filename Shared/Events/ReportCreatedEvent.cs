using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class ReportCreatedEvent
    {
        public Guid RequestId { get; set; }
        public List<HotelInformationMessage> HotelInformations { get; set; } = new List<HotelInformationMessage>();
    }
}
