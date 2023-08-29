using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class ReportFailEvent
    {
        public Guid ReportRequestId { get; set; }
        public string Message { get; set; }
    }
}
