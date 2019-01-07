using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VenuesApi.Models
{
    public class Customer : BaseEntity
    {
        [Required]
        public string Name { set; get; }

        [Required]
        [EmailAddress]
        public string Email { set; get; }

        public IEnumerable<Reservation> Reservations { set; get; }

    }
}