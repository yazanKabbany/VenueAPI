using System.Collections.Generic;
using VenuesApi.Data.Dto;
using VenuesApi.Data.Interfaces;
using VenuesApi.Models;
using System.Linq;
using System;

namespace VenuesApi.Data.Repositories
{
    public class VenueRepository : BaseRepository, IVenueRepository
    {
        public VenueRepository(VenuesDbContext context) : base(context)
        {
        }
        //Create a venue or returns false
        public int CreateVenue(VenueDto venueDto)
        {
            VenueType type;
            VenuePrivacy privacy;
            //parse type and privacy
            if (!Enum.TryParse<VenueType>(venueDto.Type, true, out type)
            ||
            !Enum.TryParse<VenuePrivacy>(venueDto.Privacy, true, out privacy))
            {
                return 0;
            }
            try
            {
                var venue = new Venue()
                {
                    Name = venueDto.Name,
                    Address = venueDto.Address,
                    Capacity = venueDto.Capacity,
                    Type = type,
                    Privacy = privacy
                };
                Context.Add(venue);
                Context.SaveChanges();
                return venue.id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //delete venue with given id
        //or returns false if no such venue
        public Status DeleteVenue(int id)
        {
            var venue = Context.Venues.SingleOrDefault(v => v.id == id);
            if (venue == null)//no such venue
            {
                return Status.NotFound;
            }
            Context.Remove(venue);//delete venue
            Context.SaveChanges();
            return Status.Success;
        }
        //return venue with given id or null if no such venue
        public VenueDto GetVenue(int id)
        {
            try
            {
                //query for the venue
                var venue = Context.Venues.SingleOrDefault(v => v.id == id);
                //create venueDto object 
                var venueDto = new VenueDto()
                {
                    id = venue.id,
                    Name = venue.Name,
                    Address = venue.Address,
                    Capacity = venue.Capacity,
                    Type = venue.Type.ToString(),
                    Privacy = venue.Privacy.ToString()
                };
                return venueDto;
            }
            catch (Exception)
            {
                //returns null if something goes wrong
                return null;
            }
        }

        public IEnumerable<VenueDto> GetVenues(string typeString)
        {
            VenueType type = 0;
            if (!String.IsNullOrEmpty(typeString) && !Enum.TryParse<VenueType>(typeString, true, out type))
            {
                //return an empty list if the type is invalid
                return new List<VenueDto>();
            }

            //query for all venues  with given type
            //or query for all venues if no type was given
            var venueDtoList = Context.Venues
            .Where(venue =>
                String.IsNullOrEmpty(typeString)
                ||
                venue.Type == type
                )
            .Select(venue =>
                new VenueDto()
                {
                    id = venue.id,
                    Name = venue.Name,
                    Address = venue.Address,
                    Capacity = venue.Capacity,
                    Type = venue.Type.ToString(),
                    Privacy = venue.Privacy.ToString()
                }
            ).ToList();
            return venueDtoList;
        }

        //update a venue or return false
        public Status UpdateVenue(int id, VenueDto venueDto)
        {
            var venue = Context.Venues.SingleOrDefault(v => v.id == id);
            if (venue == null)
            {
                return Status.NotFound;
            }
            VenueType type;
            VenuePrivacy privacy;

            //parse type and privacy
            if (!Enum.TryParse<VenueType>(venueDto.Type, true, out type)
            ||
            !Enum.TryParse<VenuePrivacy>(venueDto.Privacy, true, out privacy))
            {
                return Status.Error;
            }
            //update venue data
            venue.Name = venueDto.Name;
            venue.Address = venueDto.Address;
            venue.Capacity = venueDto.Capacity;
            venue.Type = type;
            venue.Privacy = privacy;

            Context.Update(venue);
            Context.SaveChanges();
            return Status.Success;
        }
    }
}