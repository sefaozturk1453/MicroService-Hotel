using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class ReportCompletedEvent
    {
        public Guid ReportRequestId { get; set; }
        public List<ReportMessage> ReportMessages { get; set; } = new List<ReportMessage>();
        
    }
}
