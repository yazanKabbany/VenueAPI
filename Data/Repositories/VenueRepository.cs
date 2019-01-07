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
        public bool CreateVenue(VenueDto venueDto)
        {
            VenueType type;
            VenuePrivacy privacy;
            //parse type and privacy
            if (!Enum.TryParse<VenueType>(venueDto.Type, true, out type)
            ||
            !Enum.TryParse<VenuePrivacy>(venueDto.Privacy, true, out privacy))
            {
                return false;
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
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //delete venue with given id
        //or returns false if no such venue
        public bool DeleteVenue(int id)
        {
            var venue = Context.Venues.SingleOrDefault(v => v.id == id);
            if (venue == null)//no such venue
            {
                return false;
            }
            Context.Remove(venue);//delete venue
            Context.SaveChanges();
            return true;
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
                    Privacy = venue.ToString()
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
        public bool UpdateVenue(int id, VenueDto venueDto)
        {
            var venue = Context.Venues.SingleOrDefault(v => v.id == id);
            if (venue == null)
            {
                return false;
            }
            VenueType type;
            VenuePrivacy privacy;

            //parse type and privacy
            if (!Enum.TryParse<VenueType>(venueDto.Type, true, out type)
            ||
            !Enum.TryParse<VenuePrivacy>(venueDto.Privacy, true, out privacy))
            {
                return false;
            }
            //update venue data
            venue.Name = venueDto.Name;
            venue.Address = venueDto.Address;
            venue.Capacity = venueDto.Capacity;
            venue.Type = type;
            venue.Privacy = privacy;

            Context.Update(venue);
            Context.SaveChanges();
            return true;
        }
    }
}