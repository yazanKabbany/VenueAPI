using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VenuesApi.Data;
using VenuesApi.Data.Dto;
using VenuesApi.Data.Interfaces;


namespace VenuesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        public IReservationRepository ReservationRepository { get; }

        public ReservationsController(IReservationRepository reservationRepository)
        {
            ReservationRepository = reservationRepository;
        }

        [HttpGet]
        [ProducesResponseType(200,
        Type = typeof(IEnumerable<ReservationDto>))]
        public IActionResult GetReservation(int? VenueID, int? CustomerID)
        {
            return Ok(ReservationRepository.GetReservations(VenueID, CustomerID));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReservationDto))]
        public IActionResult GetReservation(int id)
        {
            var reservation = ReservationRepository.GetReservation(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost]
        public IActionResult PostReservation([FromBody] ReservationDto reservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var id = ReservationRepository.CreateReservation(reservationDto);
            if (id == 0)
            {
                return BadRequest();
            }
            reservationDto.id = id;
            return CreatedAtAction(nameof(GetReservation), new { id = id }, reservationDto);
        }

        [HttpPut("{id}")]
        public IActionResult PutReservation(int id, [FromBody] ReservationDto reservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var status = ReservationRepository.UpdateReservation(id, reservationDto);
            if (status == Status.Error)
            {
                return BadRequest();
            }
            if (status == Status.NotFound)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var status = ReservationRepository.DeleteReservation(id);
            if (status == Status.NotFound)
            {
                return NotFound();
            }
            if (status == Status.Error)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}