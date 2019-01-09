using System;
using System.ComponentModel.DataAnnotations;
using VenuesApi.Models;

namespace VenuesApi.Data.Dto
{
    public class ReservationDto
    {
        public int id { set; get; }
        [Required]
        public int VenueID { set; get; }
        [Required]
        public int CustomerID { set; get; }
        [Required]
        public string EventName { set; get; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy}")]
        public DateTime day { set; get; }

        [Required]
        public int NumberOfPeople { set; get; }
    }
}