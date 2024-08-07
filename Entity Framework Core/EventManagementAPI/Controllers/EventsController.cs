using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventManagementAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using CsvHelper;
using System.IO;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private static List<Event> events = [];

        [HttpGet("{id}")]
        public IActionResult GetEvent(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var eventData = events.FirstOrDefault(e => e.EventId == id);

            if (eventData == null)
            {
                return NotFound();
            }

            return Ok(eventData);
        }

        [HttpGet]
        public IActionResult GetEvents([FromQuery] string? name, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            if (name != null && startDate == null && endDate == null)
            {
                var eventWithName = events.FirstOrDefault(e => e.Name == name);

                if (eventWithName == null)
                {
                    return NotFound();
                }

                return Ok(eventWithName);
            } 
            else if (name == null && startDate != null && endDate != null)
            {
                var eventByDate = events.FindAll(e => (startDate <= e.Date && e.Date <= endDate));

                if (eventByDate == null)
                {
                    return NotFound();
                }

                return Ok(eventByDate);
            }
            else if (name != null && startDate != null && endDate != null)
            {
                var eventWithName = events.FindAll(e => e.Name == name);
                if (eventWithName == null)
                {
                    return NotFound();
                }
                var eventWithNameAndDate = eventWithName.FindAll(e => (startDate <= e.Date && e.Date <= endDate));
                if (eventWithNameAndDate == null)
                {
                    return NotFound();
                }
                return Ok(eventWithNameAndDate);
            }
            else
            {
                 return Ok(events);
            }
        }

        [HttpGet]
        [Route("export")]
        public IActionResult ExportEvents([FromQuery] string format)
        {
            if (!string.IsNullOrEmpty(format))
            {
                if (format == "json")
                {
                    return Ok(events);
                }

                if (format == "csv")
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var streamWriter = new StreamWriter(memoryStream))
                        using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                        {
                            csvWriter.WriteRecords(events);
                        }
                        return File(memoryStream.ToArray(), "text/csv", "events.csv");
                    }    
                }
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] Event eventData)
        {
            events.Add(eventData);
            return Created($"events/{eventData.EventId}", eventData);
        }

        [HttpPost("{id}/tags")]
        public IActionResult AddTag(int id, [FromBody] Tag tag)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var eventData = events.FirstOrDefault(e => e.EventId == id);

            if (eventData == null)
            {
                return NotFound();
            }

            eventData.Tags.Add(tag);
            return Ok(eventData);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] Event inputEventData)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var eventData = events.FirstOrDefault(e => e.EventId == id);

            if (eventData == null)
            {
                return NotFound();
            }

            eventData.EventId = inputEventData.EventId;
            eventData.Name = inputEventData.Name;
            eventData.Date = inputEventData.Date;
            eventData.Location = inputEventData.Location;
            eventData.MaxAttendees = inputEventData.MaxAttendees;
            eventData.Tags = inputEventData.Tags;
            return Ok(eventData);
        }

        [HttpPatch("{id}/location")]
        public IActionResult UpdateLocation(int id, [FromForm] string location)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var eventData = events.FirstOrDefault(e => e.EventId == id);

            if (eventData == null)
            {
                return NotFound();
            }

            eventData.Location = location;
            return Ok(eventData);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var eventData = events.FirstOrDefault(e => e.EventId == id);

            if (eventData == null)
            {
                return NotFound();
            }

            events.Remove(eventData);
            return NoContent();
        }
    }
}
