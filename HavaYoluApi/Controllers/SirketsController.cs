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
    public class SirketsController : ApiController
    {
        private HavaYoluEntities db = new HavaYoluEntities();

        // GET: api/Sirkets
        public IQueryable<Sirket> GetSirkets()
        {
            return db.Sirkets;
        }

        // GET: api/Sirkets/5
        [ResponseType(typeof(Sirket))]
        public async Task<IHttpActionResult> GetSirket(int id)
        {
            Sirket sirket = await db.Sirkets.FindAsync(id);
            if (sirket == null)
            {
                return NotFound();
            }

            return Ok(sirket);
        }

        // PUT: api/Sirkets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSirket(int id, Sirket sirket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sirket.SirketNo)
            {
                return BadRequest();
            }

            db.Entry(sirket).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SirketExists(id))
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

        // POST: api/Sirkets
        [ResponseType(typeof(Sirket))]
        public async Task<IHttpActionResult> PostSirket(Sirket sirket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sirkets.Add(sirket);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sirket.SirketNo }, sirket);
        }

        // DELETE: api/Sirkets/5
        [ResponseType(typeof(Sirket))]
        public async Task<IHttpActionResult> DeleteSirket(int id)
        {
            Sirket sirket = await db.Sirkets.FindAsync(id);
            if (sirket == null)
            {
                return NotFound();
            }

            db.Sirkets.Remove(sirket);
            await db.SaveChangesAsync();

            return Ok(sirket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SirketExists(int id)
        {
            return db.Sirkets.Count(e => e.SirketNo == id) > 0;
        }
    }
}