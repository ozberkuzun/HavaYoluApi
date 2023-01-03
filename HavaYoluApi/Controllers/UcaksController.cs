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
    public class UcaksController : ApiController
    {
        private HavaYoluEntities db = new HavaYoluEntities();

        // GET: api/Ucaks
        public IQueryable<Ucak> GetUcaks()
        {
            return db.Ucaks;
        }

        // GET: api/Ucaks/5
        [ResponseType(typeof(Ucak))]
        public async Task<IHttpActionResult> GetUcak(int id)
        {
            Ucak ucak = await db.Ucaks.FindAsync(id);
            if (ucak == null)
            {
                return NotFound();
            }

            return Ok(ucak);
        }

        // PUT: api/Ucaks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUcak(int id, Ucak ucak)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ucak.UcakNo)
            {
                return BadRequest();
            }

            db.Entry(ucak).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UcakExists(id))
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

        // POST: api/Ucaks
        [ResponseType(typeof(Ucak))]
        public async Task<IHttpActionResult> PostUcak(Ucak ucak)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ucaks.Add(ucak);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ucak.UcakNo }, ucak);
        }

        // DELETE: api/Ucaks/5
        [ResponseType(typeof(Ucak))]
        public async Task<IHttpActionResult> DeleteUcak(int id)
        {
            Ucak ucak = await db.Ucaks.FindAsync(id);
            if (ucak == null)
            {
                return NotFound();
            }

            db.Ucaks.Remove(ucak);
            await db.SaveChangesAsync();

            return Ok(ucak);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UcakExists(int id)
        {
            return db.Ucaks.Count(e => e.UcakNo == id) > 0;
        }
    }
}