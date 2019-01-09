using System;
using System.ComponentModel.DataAnnotations;
using VenuesApi.Models;

namespace VenuesApi.Data.Dto
{
    public class ReservationDto
    {
        public int id { set; get; }
        [Required]
        public string EventName { set; get; }

        [Required]
        public DateTime day { set; get; }

        [Required]
        public int NumberOfPeople { set; get; }

        int VenueID { set; get; }

        int CustomerID { set; get; }
    }
}