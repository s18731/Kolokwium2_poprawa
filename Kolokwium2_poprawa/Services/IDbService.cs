using Kolokwium2_poprawa.DTOs;
using Kolokwium2_poprawa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2_poprawa.Services
{
    public interface IDbService
    {
        public IEnumerable<ArtistEvent> GetArtistsEvents(int id);
        public IEnumerable<Event> GetEventInfoByArtist(int id);
        public bool IsEventStarted (int id);
        public Event UpdateEventInfo(UpdateArtistEventTimeRequest request);
    }
}
