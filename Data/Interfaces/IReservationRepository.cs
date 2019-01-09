
using System.Collections.Generic;
using VenuesApi.Data.Dto;

namespace VenuesApi.Data.Interfaces
{
    public interface IReservationRepository
    {
        ReservationDto GetReservation(int id);
        IEnumerable<ReservationDto> GetReservations(int? VenueId, int? CustomerId);
        Status DeleteReservation(int id);
        int CreateReservation(ReservationDto ReservationDto);
        Status UpdateReservation(int id, ReservationDto ReservationDto);
    }
}
