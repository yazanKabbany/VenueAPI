using System.ComponentModel.DataAnnotations;

namespace VenuesApi.Data.Dto
{
    public class VenueDto
    {

        [Required]
        public int id { set; get; }
        [Required]
        public string Name { set; get; }

        [Required]
        public string Address { set; get; }

        [Required]
        public int Capacity { set; get; }

        [Required]
        public string Type { set; get; }

        [Required]
        public string Privacy { set; get; }

    }
}