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

namespace A2_HTML5_Biz_App_New.Controllers
{
    public class OwnerPropertyDescriptionAPIController : ApiController
    {
        private PropertyStoreEntities db = new PropertyStoreEntities();

        // GET: api/OwnerPropertyDescriptionAPI
        public List<OwnerPropertyDescription> GetOwnerPropertyDescriptions()
        {

            //Logic for Getting the Current Login User and its Owner Is
            var user = this.User.Identity.Name;
            var owner = (from o in db.OwnerInfoes.ToList()
                         where o.Email == user
                         select new OwnerInfo()
                         {
                             OwnerId = o.OwnerId
                         }).First();
            //Ends Here

            var OwnPropeDesc = from owpd in db.OwnerPropertyDescriptions.ToList()
                               where owpd.OwnerId == owner.OwnerId
                               select new OwnerPropertyDescription()
                               {
                                   OwnerPropertyId = owpd.OwnerPropertyId,
                                   PropertyType = owpd.PropertyType,
                                   OwnerId = owpd.OwnerId,
                                   Address = owpd.Address,
                                   BedRoomNo = owpd.BedRoomNo,
                                   TotalRooms = owpd.TotalRooms,
                                   PropertyBuildupArea = owpd.PropertyBuildupArea,
                                   PropertyDescription = owpd.PropertyDescription,
                                   PropertySaleRentStatus = owpd.PropertySaleRentStatus,
                                   SaleOrRentCost = owpd.SaleOrRentCost,
                                   PropertyAge = owpd.PropertyAge,
                                   Status = owpd.Status,
                                   RegistrationDate = owpd.RegistrationDate
                               };

            return OwnPropeDesc.ToList();
        }

        // GET: api/OwnerPropertyDescriptionAPI/5
        [ResponseType(typeof(OwnerPropertyDescription))]
        public IHttpActionResult GetOwnerPropertyDescription(int id)
        {
            OwnerPropertyDescription ownerPropertyDescription = db.OwnerPropertyDescriptions.Find(id);
            if (ownerPropertyDescription == null)
            {
                return NotFound();
            }

            return Ok(ownerPropertyDescription);
        }

        // PUT: api/OwnerPropertyDescriptionAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOwnerPropertyDescription(int id, OwnerPropertyDescription ownerPropertyDescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ownerPropertyDescription.OwnerPropertyId)
            {
                return BadRequest();
            }

            db.Entry(ownerPropertyDescription).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerPropertyDescriptionExists(id))
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

        // POST: api/OwnerPropertyDescriptionAPI
        [ResponseType(typeof(OwnerPropertyDescription))]
        public IHttpActionResult PostOwnerPropertyDescription(OwnerPropertyDescription ownerPropertyDescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ownerPropertyDescription.RegistrationDate = DateTime.Now;
            db.OwnerPropertyDescriptions.Add(ownerPropertyDescription);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ownerPropertyDescription.OwnerPropertyId }, ownerPropertyDescription);
        }

        // DELETE: api/OwnerPropertyDescriptionAPI/5
        [ResponseType(typeof(OwnerPropertyDescription))]
        public IHttpActionResult DeleteOwnerPropertyDescription(int id)
        {
            OwnerPropertyDescription ownerPropertyDescription = db.OwnerPropertyDescriptions.Find(id);
            if (ownerPropertyDescription == null)
            {
                return NotFound();
            }

            db.OwnerPropertyDescriptions.Remove(ownerPropertyDescription);
            db.SaveChanges();

            return Ok(ownerPropertyDescription);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OwnerPropertyDescriptionExists(int id)
        {
            return db.OwnerPropertyDescriptions.Count(e => e.OwnerPropertyId == id) > 0;
        }
    }
}