using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VenuesApi.Models
{
    public class Venue : BaseEntity
    {
        [Required]
        public string Name { set; get; }

        [Required]
        public string Address { set; get; }

        [Required]
        public int Capacity { set; get; }

        [Required]
        public VenueType Type { set; get; }

        [Required]
        public VenuePrivacy Privacy { set; get; }

        public IEnumerable<Reservation> Reservations { set; get; }

    }
}

public enum VenueType
{
    Hotel,
    Restaurant
}
public enum VenuePrivacy
{
    Private,
    SemiPrivate,
    Public
}