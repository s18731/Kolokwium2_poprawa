using Kolokwium2_poprawa.DTOs;
using Kolokwium2_poprawa.Exceptions;
using Kolokwium2_poprawa.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2_poprawa.Services
{
    public class EfArtistDbService : IDbService
    {
        private readonly s18731Context _context;
        public EfArtistDbService(s18731Context context)
        {
            _context = context;
        }

        /*
         _dbContext.Entry(student).Property("FirstName").IsModified = true;
             */

        public IEnumerable<ArtistEvent> GetArtistsEvents(int id)
        {
            if (id < 0)
            {
                throw new ArgumentCannotBeNegativeException("Argument of given query cannot be negative!");
            }
            var artistsEvents = _context.ArtistEvent.Where(c => c.IdArtist == id).ToList();

            if (artistsEvents == null)
            {
                throw new NoArtistsEventException("Artist with given id did not perform in any events!");
            }

            return artistsEvents;
        }

        public IEnumerable<Event> GetEventInfoByArtist(int id)
        {
            var artistsEvents = GetArtistsEvents(id);
            List <Event> returnedEvents = new List<Event>();

            foreach (ArtistEvent artistE in artistsEvents)
            {
                var returnedEventInfo = _context.Event.Where(c => c.IdEvent == artistE.IdEvent).FirstOrDefault();
                returnedEvents.Add(returnedEventInfo);
            }

            if (returnedEvents == null)
            {
                throw new NoArtistsEventException("No events are associated with given artist!");
            }

            return returnedEvents.OrderByDescending(c => c.StartDate);
        }

        public bool IsEventStarted(int id)
        {
            if (id < 0)
            {
                throw new ArgumentCannotBeNegativeException("Argument of given query cannot be negative!");
            }
            var eventDate = _context.Event.Where(c => c.IdEvent == id).Select(c => c.StartDate).FirstOrDefault();

            DateTime currDate = DateTime.Now;

            var value = (eventDate - currDate).TotalSeconds;

            if (value < 0)
                return true;
            else
                return false;
        }

        public Event UpdateEventInfo(UpdateArtistEventTimeRequest request)
        {
            if (request.idArtist == null || request.idArtist == null || request.idEvent == null)
                throw new ArgumentNullException("Neither of the arguments can be null");
            if (request.idEvent < 0 || request.idArtist < 0)
                throw new ArgumentCannotBeNegativeException("Neither of the IDs can be negative");
            var eventId = _context.Event.Where(c => c.IdEvent == request.idEvent).Select(c => c.IdEvent).FirstOrDefault();
            if (!IsEventStarted(eventId))
            {
                var dateDiff = (request.performanceDate - DateTime.Now).TotalSeconds;
                if (dateDiff < 0)
                {
                    throw new StartDateFurtherThanEndDateException("Start date cannot be further than end date");
                }

                var queriedEvent = _context.Event.Where(id => id.IdEvent == request.idEvent).FirstOrDefault();

                if (queriedEvent == null)
                    throw new EventNotFoundException("Cannot find event with given ID");

                queriedEvent.StartDate = request.performanceDate;
                _context.Entry(queriedEvent).Property("StartDate").IsModified = true;
                _context.Attach(queriedEvent);
                _context.SaveChanges();

                var updatedEvent = _context.Event.Where(c => c.IdEvent == request.idEvent).FirstOrDefault();

                return updatedEvent;
            }
            else
            {
                throw new EventAlreadyStartedException("Cannot update event that already has started!");
            }
            return null;
        }
    }
}
