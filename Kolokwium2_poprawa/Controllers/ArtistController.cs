using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium2_poprawa.DTOs;
using Kolokwium2_poprawa.Exceptions;
using Kolokwium2_poprawa.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium2_poprawa.Controllers
{
    [Route("api/artists")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IDbService _context;

        public ArtistController(IDbService context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetEventsInfoForGivenArtist(int id)
        {
            try
            {
                var eventList = _context.GetEventInfoByArtist(id);
                return Ok(eventList);
            }
            catch (ArgumentCannotBeNegativeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NoArtistsEventException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{id}/events/{id2}")]
        public IActionResult UpdateEventInfo(UpdateArtistEventTimeRequest eventTime)
        {
            // wykorzystałem id podane w requeście nie w ciele żądania
            try 
            {
                _context.UpdateEventInfo(eventTime);
                return Ok("Event updated successfully!");
            }
            catch (ArgumentCannotBeNegativeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EventAlreadyStartedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EventNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (StartDateFurtherThanEndDateException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}