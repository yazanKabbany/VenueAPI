
using System.Collections.Generic;
using VenuesApi.Data.Dto;

namespace VenuesApi.Data.Interfaces
{
    public interface IVenueRepository
    {
        VenueDto GetVenue(int id);
        IEnumerable<VenueDto> GetVenues(string type);
        Status DeleteVenue(int id);
        int CreateVenue(VenueDto venue);
        Status UpdateVenue(int id, VenueDto venue);
    }
}
