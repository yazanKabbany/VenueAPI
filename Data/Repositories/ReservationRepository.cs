using System.Collections.Generic;
using VenuesApi.Data.Dto;
using VenuesApi.Data.Interfaces;
using VenuesApi.Models;
using System.Linq;
using System;

namespace VenuesApi.Data.Repositories
{
    public class ReservationRepository : BaseRepository, IReservationRepository
    {
        public ReservationRepository(VenuesDbContext context) : base(context)
        {
        }
        //create a new reservation and return its id
        //or return 0 if something is wrong
        public int CreateReservation(ReservationDto reservationDto)
        {
            var venue = Context.Venues.SingleOrDefault(v => v.id == reservationDto.VenueID);
            if (venue == null)
            {
                return 0;
            }
            var customer = Context.Customers.SingleOrDefault(c => c.id == reservationDto.CustomerID);
            if (customer == null)
            {
                return 0;
            }
            if (venue.Capacity < reservationDto.NumberOfPeople)
            {
                return 0;
            }
            if (Context.Reservations.Any(r =>
                    r.day.Date == reservationDto.day.Date
                    &&
                    r.VenueID == reservationDto.VenueID))
            {
                return 0;
            }
            var reservation = new Reservation()
            {
                EventName = reservationDto.EventName,
                day = reservationDto.day.Date,
                NumberOfPeople = reservationDto.NumberOfPeople,
                Venue = venue,
                Customer = customer
            };

            Context.Add(reservation);
            Context.SaveChanges();
            return reservation.id;
        }
        //delete reservation with given id 
        public Status DeleteReservation(int id)
        {
            var reservation = Context.Reservations.SingleOrDefault(r => r.id == id);
            if (reservation == null)
            {
                return Status.NotFound;
            }
            Context.Remove(reservation);
            Context.SaveChanges();
            return Status.Success;
        }
        //get reservation with given id 
        public ReservationDto GetReservation(int id)
        {
            var reservation = Context.Reservations.SingleOrDefault(r => r.id == id);
            if (reservation == null)
            {
                return null;
            }
            var reservationDto = new ReservationDto()
            {
                id = reservation.id,
                VenueID = reservation.VenueID,
                CustomerID = reservation.CustomerID,
                EventName = reservation.EventName,
                day = reservation.day,
                NumberOfPeople = reservation.NumberOfPeople

            };
            return reservationDto;
        }
        //get all reservations #endregion
        //optional filtering with VenueId and CustomerId
        public IEnumerable<ReservationDto> GetReservations(int? VenueId, int? CustomerId)
        {
            var reservationDtoList = Context.Reservations
            .Where(r =>
                (VenueId == null || r.VenueID == VenueId)
                &&
                (CustomerId == null || r.CustomerID == CustomerId))
            .Select(reservation => new ReservationDto()
            {
                id = reservation.id,
                VenueID = reservation.VenueID,
                CustomerID = reservation.CustomerID,
                EventName = reservation.EventName,
                day = reservation.day,
                NumberOfPeople = reservation.NumberOfPeople
            });
            return reservationDtoList;
        }
        //update reservation
        public Status UpdateReservation(int id, ReservationDto reservationDto)
        {
            var reservation = Context.Reservations.SingleOrDefault(r => r.id == id);
            if (reservation == null)
            {
                return Status.NotFound;
            }
            var venue = Context.Venues.SingleOrDefault(v => v.id == reservationDto.VenueID);
            if (venue == null)
            {
                return Status.Error;
            }
            var customer = Context.Customers.SingleOrDefault(c => c.id == reservationDto.CustomerID);
            if (customer == null)
            {
                return Status.Error;
            }
            if (venue.Capacity < reservationDto.NumberOfPeople)
            {
                return Status.Error;
            }
            var same_day_reservation = Context.Reservations
                    .Where(r =>
                        r.day.Date == reservationDto.day.Date
                        &&
                        r.VenueID == reservationDto.VenueID)
                    .SingleOrDefault();
            if (same_day_reservation != null && same_day_reservation.id != id)
            {
                return Status.Error;
            }

            reservation.Venue = venue;
            reservation.Customer = customer;
            reservation.EventName = reservationDto.EventName;
            reservation.day = reservationDto.day;
            reservation.NumberOfPeople = reservationDto.NumberOfPeople;

            Context.Update(reservation);
            Context.SaveChanges();

            return Status.Success;
        }
    }

}