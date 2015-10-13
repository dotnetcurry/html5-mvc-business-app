using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using A2_HTML5_Biz_App_New.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace A2_HTML5_Biz_App_New.Controllers
{
    public class OwnerInfoAPIController : ApiController
    {
        private PropertyStoreEntities db = new PropertyStoreEntities();

        // GET: api/OwnerInfoAPI
        public IQueryable<OwnerInfo> GetOwnerInfoes()
        {
            return db.OwnerInfoes;
        }
 

        // GET: api/OwnerInfoAPI/5
        [ResponseType(typeof(OwnerInfo))]
        public IHttpActionResult GetOwnerInfo(int id)
        {
            OwnerInfo ownerInfo = db.OwnerInfoes.Find(id);
            if (ownerInfo == null)
            {
                return NotFound();
            }

            return Ok(ownerInfo);
        }

        // PUT: api/OwnerInfoAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOwnerInfo(int id, OwnerInfo ownerInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ownerInfo.OwnerId)
            {
                return BadRequest();
            }

            db.Entry(ownerInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerInfoExists(id))
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

        // POST: api/OwnerInfoAPI
        [ResponseType(typeof(OwnerInfo))]
        public IHttpActionResult PostOwnerInfo(OwnerInfo ownerInfo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                ownerInfo.RegistrationDate = DateTime.Now;

                db.OwnerInfoes.Add(ownerInfo);
                db.SaveChanges();
            }
            catch (DbEntityValidationException  e)
            {
               foreach (var eve in e.EntityValidationErrors)
    {
        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            eve.Entry.Entity.GetType().Name, eve.Entry.State);
        foreach (var ve in eve.ValidationErrors)
        {
            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                ve.PropertyName, ve.ErrorMessage);
        }
    }
    throw; 
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = ownerInfo.OwnerId }, ownerInfo);
        }

        // DELETE: api/OwnerInfoAPI/5
        [ResponseType(typeof(OwnerInfo))]
        public IHttpActionResult DeleteOwnerInfo(int id)
        {
            OwnerInfo ownerInfo = db.OwnerInfoes.Find(id);
            if (ownerInfo == null)
            {
                return NotFound();
            }

            db.OwnerInfoes.Remove(ownerInfo);
            db.SaveChanges();

            return Ok(ownerInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OwnerInfoExists(int id)
        {
            return db.OwnerInfoes.Count(e => e.OwnerId == id) > 0;
        }
    }
}