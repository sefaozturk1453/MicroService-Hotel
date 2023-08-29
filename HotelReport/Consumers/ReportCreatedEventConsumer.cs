using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Events;

namespace HotelReport.Consumers
{
    public class ReportCreatedEventConsumer: IConsumer<ReportCreatedEvent>
    {
        private ILogger<ReportCreatedEventConsumer> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;
        public ReportCreatedEventConsumer(ILogger<ReportCreatedEventConsumer> logger, IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Consume(ConsumeContext<ReportCreatedEvent> context)
        {
            try
            {
                var informations = context.Message.HotelInformations;

                var locationInfo =
                    from info in informations
                    where info.Type == 2
                    orderby info.Info
                    group info by info.Info
                    into locationGroup
                    select new
                    {
                        Location = locationGroup.Key,
                        HotelCount = locationGroup.GroupBy(x => x.HotelId).Count()

                    };



                var completedEvents = new ReportCompletedEvent();
                var messages = new List<ReportMessage>();
                foreach (var lokasyondakiler in locationInfo)
                {
                    var message = new ReportMessage
                    {
                        Location = lokasyondakiler.Location,
                        HotelCount = lokasyondakiler.HotelCount,
                        NumberCount = 
                        (from loc in informations.Where(x => x.Type == 2 && x.Info == lokasyondakiler.Location)
                         join num in informations.Where(x => x.Type == 0)
                             on loc.HotelId equals num.HotelId
                         select loc.HotelId).ToList().Count
                    };
                    messages.Add(message);

                }
                messages.ForEach(item =>
                {
                    completedEvents.ReportMessages.Add(new ReportMessage() { Location = item.Location, NumberCount = item.NumberCount, HotelCount = item.HotelCount });
                });
                completedEvents.ReportRequestId = context.Message.RequestId;

                _logger.LogInformation("Report created.");

                var sendEndpoint =
                    await _sendEndpointProvider.GetSendEndpoint(
                        new Uri($"queue:{RabbitMQSettingsConst.ReportRequestCompletedEventQueueName}"));

                await sendEndpoint.Send(completedEvents);
            }
            catch (Exception e)
            {
                await _publishEndpoint.Publish(new ReportFailEvent()
                {
                    Message = e.Message,
                    ReportRequestId = context.Message.RequestId
                });
                throw;
            }



        }
        public enum InfoType
        {
            Phone,
            EMail,
            Location
        }
    }
}
