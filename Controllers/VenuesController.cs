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
    public class VenuesController : ControllerBase
    {
        public IVenueRepository VenueRepository { get; }
        public VenuesController(IVenueRepository venueRepository)
        {
            VenueRepository = venueRepository;
        }
        //get all venues or venus with specific type
        [HttpGet]
        [ProducesResponseType(200,
        Type = typeof(IEnumerable<VenueDto>))]
        public IActionResult GetVenues(string type)
        {
            return Ok(VenueRepository.GetVenues(type));
        }
        //get venue with given id or return 404
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(VenueDto))]
        public IActionResult GetVenue(int id)
        {
            var venue = VenueRepository.GetVenue(id);
            if (venue == null)
            {
                return NotFound();
            }
            return Ok(venue);
        }
        //Create a new Venue
        [HttpPost]
        public IActionResult PostVenue([FromBody] VenueDto venueDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = VenueRepository.CreateVenue(venueDto);
            if (id == 0)
            {
                return BadRequest();
            }
            venueDto.id = id;
            return CreatedAtAction(nameof(GetVenue), new { id = id }, venueDto);
        }
        //Update existent Venue
        [HttpPut("{id}")]
        public IActionResult PutVenue(int id, [FromBody] VenueDto venueDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var status = VenueRepository.UpdateVenue(id, venueDto);
            if(status == Status.Error)
            {
                return BadRequest();
            }
            if(status == Status.NotFound)
            {
                return NotFound();
            }
            return NoContent();
        }
        //Delete existent Venue
        [HttpDelete("{id}")]
        public IActionResult DeleteVenue(int id)
        {
            var status = VenueRepository.DeleteVenue(id);
            if(status == Status.Error)
            {
                return BadRequest();
            }
            if(status == Status.NotFound)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}