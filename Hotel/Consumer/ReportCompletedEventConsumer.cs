using System;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Models;
using Hotel.Models.Contexts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Events;

namespace Hotel.Consumer
{
    public class ReportCompletedEventConsumer : IConsumer<ReportCompletedEvent>
    {
        private readonly HotelContext _context;
        private readonly ILogger<ReportCompletedEventConsumer> _logger;

        public ReportCompletedEventConsumer(HotelContext context, ILogger<ReportCompletedEventConsumer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ReportCompletedEvent> context)
        {
            var anyRequest = await _context.ReportRequests.AnyAsync(x => x.Id == context.Message.ReportRequestId);
            if (!anyRequest)
            {
                _logger.LogInformation("Report not recorded.");
            }

            var requestReport =
                await _context.ReportRequests.SingleOrDefaultAsync(x => x.Id == context.Message.ReportRequestId);
            requestReport.Status = StatusType.Done;
            await _context.SaveChangesAsync();

            foreach (var report in context.Message.ReportMessages.Select(item => new ReportDetail()
                         {
                             Id = Guid.NewGuid(),
                             ReportRequestId = context.Message.ReportRequestId,
                             Location = item.Location,
                             NumberCount = item.NumberCount,
                             HotelCount= item.HotelCount
            }
                     )
                    )
            {
                await _context.AddAsync(report);
                await _context.SaveChangesAsync();
            }

            _logger.LogInformation("Report recorded.");

        }
    }
}
