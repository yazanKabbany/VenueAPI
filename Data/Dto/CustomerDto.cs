using System.ComponentModel.DataAnnotations;

namespace VenuesApi.Data.Dto
{
    public class CustomerDto
    {

        public int id { set; get; }
        [Required]
        public string Name { set; get; }

        [Required]
        [EmailAddress]
        public string Email { set; get; }

    }
}