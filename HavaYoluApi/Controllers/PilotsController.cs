using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HavaYoluApi.Models;

namespace HavaYoluApi.Controllers
{
    public class PilotsController : ApiController
    {
        private HavaYoluEntities db = new HavaYoluEntities();

        // GET: api/Pilots
        public IQueryable<Pilot> GetPilots()
        {
            return db.Pilots;
        }

        // GET: api/Pilots/5
        [ResponseType(typeof(Pilot))]
        public async Task<IHttpActionResult> GetPilot(int id)
        {
            Pilot pilot = await db.Pilots.FindAsync(id);
            if (pilot == null)
            {
                return NotFound();
            }

            return Ok(pilot);
        }

        // PUT: api/Pilots/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPilot(int id, Pilot pilot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pilot.PilotNo)
            {
                return BadRequest();
            }

            db.Entry(pilot).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PilotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Pilots
        [ResponseType(typeof(Pilot))]
        public async Task<IHttpActionResult> PostPilot(Pilot pilot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pilots.Add(pilot);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pilot.PilotNo }, pilot);
        }

        // DELETE: api/Pilots/5
        [ResponseType(typeof(Pilot))]
        public async Task<IHttpActionResult> DeletePilot(int id)
        {
            Pilot pilot = await db.Pilots.FindAsync(id);
            if (pilot == null)
            {
                return NotFound();
            }

            db.Pilots.Remove(pilot);
            await db.SaveChangesAsync();

            return Ok(pilot);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PilotExists(int id)
        {
            return db.Pilots.Count(e => e.PilotNo == id) > 0;
        }
    }
}