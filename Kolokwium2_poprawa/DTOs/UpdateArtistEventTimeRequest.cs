using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2_poprawa.DTOs
{
    public class UpdateArtistEventTimeRequest
    {
        public int idArtist { get; set; }
        public int idEvent { get; set; }
        public DateTime performanceDate { get; set; }
    }
}
