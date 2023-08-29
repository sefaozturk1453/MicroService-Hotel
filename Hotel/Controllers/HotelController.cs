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

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : Controller
    {
        private readonly HotelContext _context;

        public HotelController(HotelContext context)
        {
            _context = context;
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





        //Otellerin bulundukları konuma göre istatistiklerini çıkartan bir rapor talebi

        //    Sistemin oluşturduğu raporların listelenmesi

        //    Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi
    }
}
