
using System.Collections.Generic;
using VenuesApi.Data.Dto;

namespace VenuesApi.Data.Interfaces
{
    public interface IVenueRepository
    {
        VenueDto GetVenue(int id);
        IEnumerable<VenueDto> GetVenues(string type);
        bool DeleteVenue(int id);
        bool CreateVenue(VenueDto venue);
    }
}
