using System;
using System.ComponentModel.DataAnnotations;

namespace VenuesApi.Models
{
    public class Reservation : BaseEntity
    {
        [Required]
        public string EventName { set; get; }

        [Required]
        public DateTime day { set; get; }

        [Required]
        public int NumberOfPeople { set; get; }

        public int VenueID { set; get; }
        public Venue Venue { set; get; }

        public int CustomerID { set; get; }
        public Customer Customer { set; get; }

    }
}