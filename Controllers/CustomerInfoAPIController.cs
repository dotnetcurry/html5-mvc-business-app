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
    public class CustomerInfoAPIController : ApiController
    {
        private PropertyStoreEntities db = new PropertyStoreEntities();

        // GET: api/CustomerInfoAPI
        public IQueryable<CustomerInfo> GetCustomerInfoes()
        {
            return db.CustomerInfoes;
        }

        // GET: api/CustomerInfoAPI/5
        [ResponseType(typeof(CustomerInfo))]
        public IHttpActionResult GetCustomerInfo(int id)
        {
            CustomerInfo customerInfo = db.CustomerInfoes.Find(id);
            if (customerInfo == null)
            {
                return NotFound();
            }

            return Ok(customerInfo);
        }

        // PUT: api/CustomerInfoAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomerInfo(int id, CustomerInfo customerInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerInfo.CustomerId)
            {
                return BadRequest();
            }

            db.Entry(customerInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInfoExists(id))
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

        // POST: api/CustomerInfoAPI
        [ResponseType(typeof(CustomerInfo))]
        public IHttpActionResult PostCustomerInfo(CustomerInfo customerInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            customerInfo.RegistrationDate = DateTime.Now;
            db.CustomerInfoes.Add(customerInfo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customerInfo.CustomerId }, customerInfo);
        }

        // DELETE: api/CustomerInfoAPI/5
        [ResponseType(typeof(CustomerInfo))]
        public IHttpActionResult DeleteCustomerInfo(int id)
        {
            CustomerInfo customerInfo = db.CustomerInfoes.Find(id);
            if (customerInfo == null)
            {
                return NotFound();
            }

            db.CustomerInfoes.Remove(customerInfo);
            db.SaveChanges();

            return Ok(customerInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerInfoExists(int id)
        {
            return db.CustomerInfoes.Count(e => e.CustomerId == id) > 0;
        }
    }
}