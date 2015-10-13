using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using A2_HTML5_Biz_App_New.Models;
using System.Web.Http.Description;

namespace A2_HTML5_Biz_App_New.Controllers
{
    public class CustomAPIController : ApiController
    {
        PropertyStoreEntities ctx;

        public CustomAPIController()
        {
            ctx = new PropertyStoreEntities(); 
        }

        // Action Method to get the Owner Information based upon the Login Email
        [Route("Owner/Get")]
        [ResponseType(typeof(OwnerInfo))]
        public IHttpActionResult GetOwner()
        {
            try
            {
                var user = this.User.Identity.Name;
                var Owner = (from o in ctx.OwnerInfoes.ToList()
                             where o.Email == user
                             select new OwnerInfo()
                             {
                                 OwnerId = o.OwnerId,
                                 OwnerName = o.OwnerName,
                                 Address = o.Address,
                                 City = o.City,
                                 Email = o.Email,
                                 Contact1 = o.Contact1,
                                 Contact2 = o.Contact2
                             }).First();
                return Ok(Owner);
            }
            catch (Exception)
            {
                return Ok(new OwnerInfo());
            }
        }
       
        //Get the Customer Info based upon the login Email
      
        [Route("Customer/Get")]
        [ResponseType(typeof(CustomerInfo))]
        public IHttpActionResult GetCustomer()
        {
            try
            {
                var user = this.User.Identity.Name;
                var Customer = (from c in ctx.CustomerInfoes.ToList()
                               where c.Email == user
                               select new CustomerInfo()
                               {
                                    CustomerId = c.CustomerId,
                                    CustomerName = c.CustomerName,
                                    Address = c.Address,
                                    City = c.City,
                                    Contact1 = c.Contact1,
                                    Contact2 = c.Contact2,
                                    Email = c.Email
                               }).First();
                return Ok(Customer);
            }
            catch (Exception)
            {
                 return Ok(new CustomerInfo());
            }
        }
        


         
        //In the below method the propertytype will be : Flat, Row House, Bunglow
        // filter will be : AND, OR searchtype will be : Sale, Rent

        [Route("Property/{propertytype}/{filter}/{searchtype}")]
        public List<SearchProperty> GetProperties(string propertytype, string searchtype,string filter)
        {
            List<SearchProperty> Properties = new List<SearchProperty>();

            switch (filter)
            { 
            
                case "AND":
                    Properties = (from p in ctx.OwnerPropertyDescriptions.ToList()
                                  where 
                                  p.Status == searchtype && p.PropertyType == propertytype && p.Status == "Available"
                                  select new SearchProperty()
                                  {
                                      PropertyId = p.OwnerPropertyId,
                                      OwnerName = ctx.OwnerInfoes.Find(p.OwnerId).OwnerName,
                                      Address = p.Address,
                                      Contact1 = ctx.OwnerInfoes.Find(p.OwnerId).Contact1,
                                      Contact2 = ctx.OwnerInfoes.Find(p.OwnerId).Contact2,
                                      Email = ctx.OwnerInfoes.Find(p.OwnerId).Email,
                                      BedRoomNo = p.BedRoomNo,
                                      BuildupArea = p.PropertyBuildupArea,
                                      PropertyType = p.PropertyType,
                                      SaleRentStatus = p.PropertySaleRentStatus,
                                      TotalRooms = p.TotalRooms,
                                      Status = p.Status
                                  }).ToList();
                    break;

                case "OR":
                    Properties = (from p in ctx.OwnerPropertyDescriptions.ToList()
                                  where (p.Status == searchtype || p.PropertyType == propertytype) && p.Status == "Available"
                                  select new SearchProperty()
                                  {
                                      PropertyId = p.OwnerPropertyId,
                                      OwnerName = ctx.OwnerInfoes.Find(p.OwnerId).OwnerName,
                                      Address = p.Address,
                                      Contact1 = ctx.OwnerInfoes.Find(p.OwnerId).Contact1,
                                      Contact2 = ctx.OwnerInfoes.Find(p.OwnerId).Contact2,
                                      Email = ctx.OwnerInfoes.Find(p.OwnerId).Email,
                                      BedRoomNo = p.BedRoomNo,
                                      BuildupArea = p.PropertyBuildupArea,
                                      PropertyType = p.PropertyType,
                                      SaleRentStatus = p.PropertySaleRentStatus,
                                      TotalRooms = p.TotalRooms,
                                      Status = p.Status
                                  }).ToList();
                    break;
            }
            return Properties;
        }

    }
}
