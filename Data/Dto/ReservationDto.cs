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

        public VenueSummaryDto VenueSummary { set; get; }

        public CustomerSummaryDto CustomerSummary { set; get; }
    }

    public class VenueSummaryDto : BaseSummryDto
    {

    }
    public class CustomerSummaryDto : BaseSummryDto
    {

    }

}