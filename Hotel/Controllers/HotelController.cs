using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Hotel.Models.Contexts;
using System.Threading.Tasks;
using Hotel.DTOs;
using Hotel.Helpers;
using Hotel.Models;
using System.Collections.Generic;
using MassTransit.Transports;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Events;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : Controller
    {
        private readonly HotelContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public HotelController(HotelContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("AddHotel")]
        public Task<IActionResult> AddHotel(HotelDto hotel)
        {
            var newHotel = new Models.Hotel
            {
                Id = Guid.NewGuid(),
                Name = StringHelper.BosluklariSil(hotel.Name)
            };

            _context.Add(newHotel);
            _context.SaveChanges();

            return Task.FromResult<IActionResult>(Ok());
        }

        [HttpDelete("DeleteHotel")]
        public Task<IActionResult> DeleteHotel(Guid guid)
        {
            var hotel = _context.Hotels.Any(x => x.Id == guid);
            if (!hotel)
            {
                return Task.FromResult<IActionResult>(BadRequest("Hotel mevcut değil"));
            }

            _context.Hotels.Remove(_context.Hotels.SingleOrDefault(x => x.Id == guid));
            _context.SaveChanges();



            return Task.FromResult<IActionResult>(Ok());
        }

        [HttpPost("AddHotelContact")]
        public Task<IActionResult> AddHotelContact(HotelContactDto contactDto)
        {
            var hotel = _context.Hotels.Any(x => x.Id == contactDto.Id);
            if (!hotel)
            {
                return Task.FromResult<IActionResult>(BadRequest("Hotel mevcut değil"));
            }

            if (!TypeHelper.InfoTypeCheck(contactDto.Type))
            {
                return Task.FromResult<IActionResult>(NotFound("İletişim tipi mevcut değil"));
            }

            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                HotelId = contactDto.Id,
                Type = (Models.InfoType)contactDto.Type,
                Info = contactDto.Info
            };
            _context.Add(contact);
            _context.SaveChanges();

            return Task.FromResult<IActionResult>(Ok());
        }

        [HttpDelete("DeleteHotelContact")]
        public Task<IActionResult> DeleteHotelContact(Guid guid)
        {
            var contact = _context.Contacts.Any(x => x.Id == guid);
            if (!contact)
            {
                return Task.FromResult<IActionResult>(NotFound("İletişim mevcut değil"));
            }

            _context.Remove(_context.Contacts.SingleOrDefault(x => x.Id == guid));
            _context.SaveChanges();

            return Task.FromResult<IActionResult>(Ok());
        }

        [HttpGet("GetOfficialList")]
        public Task<IActionResult> GetOfficialList()
        {
            var officialList = _context.Officials.ToList();
            var officials = officialList.Select(item => new OfficelResponseDto
            {
                Id = item.Id,
                Name = item.Name,
                Surname = item.Surname,
                Title = item.Title
            }).ToList();
            return Task.FromResult<IActionResult>(Ok(officials));
        }

        [HttpGet("GetHotelContactList")]
        public Task<IActionResult> GetHotelContactList(Guid guid)
        {
            var hotelCheck = _context.Hotels.Any(x => x.Id == guid);
            if (!hotelCheck)
            {
                return Task.FromResult<IActionResult>(NotFound("Hotel mevcut değil"));
            }

            var hotel = _context.Hotels.SingleOrDefault(x => x.Id == guid);
            var contacts = _context.Contacts.Where(x => x.HotelId == guid).ToList();

            var responseDto = new HotelContactResponseDto
            {
                Id = hotel.Id,
                Name = hotel.Name

            };

            foreach (var contact in contacts.Select(item => new HotelContactResDto
            {
                Id = item.Id,
                Type = ((InfoType)item.Type).ToString() ,
                Info = item.Info,
            }))
            {
                responseDto.Contacts.Add(contact);
            }

            return Task.FromResult<IActionResult>(Ok(responseDto));
        }

        [HttpGet("GetLocationReport")]
        public async Task<IActionResult> GetLocationReport()
        {
            var requestReport = new ReportRequest()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Status = (byte)StatusType.Preparing
            };

            await _context.AddAsync(requestReport);
            await _context.SaveChangesAsync();


            var personInformationList = await _context.Contacts.Where(x=>x.Type != InfoType.EMail).ToListAsync();

            var reportCreatedEvent = new ReportCreatedEvent();

            personInformationList.ForEach(item =>
            {
                reportCreatedEvent.HotelInformations.Add(new HotelInformationMessage() { Type = (byte)item.Type, Info = item.Info, HotelId = item.HotelId });
            });

            reportCreatedEvent.RequestId = requestReport.Id;


            await _publishEndpoint.Publish(reportCreatedEvent);

            return Ok();
        }

        [HttpGet("GetReportList")]
        public async Task<IActionResult> GetReportList()
        {

            var reports = await _context.ReportRequests.ToListAsync();

            var response = new List<ReportRequestResponseDto>();

            reports.ForEach(item =>
            {
                response.Add(new ReportRequestResponseDto() { Id = item.Id, CreatedDate = item.CreatedDate, Status = ((StatusType)item.Status).ToString() });
            });

            return Ok(response);
        }

        [HttpGet("GetReportDetail")]
        public async Task<IActionResult> GetReportDetail(Guid id)
        {

            var reports = await _context.ReportDetails.Where(x => x.ReportRequestId == id).ToListAsync();

            var response = new List<ReportDetail>();

            reports.ForEach(item =>
            {
                response.Add(new ReportDetail() { Id = item.Id, Location = item.Location, HotelCount = item.HotelCount, NumberCount = item.NumberCount });
            });

            return Ok(response);
        }


        //    Sistemin oluşturduğu raporların listelenmesi

        //    Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi
    }
}
