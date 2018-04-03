using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    public class BusinessOfferLocationController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public BusinessOfferLocationController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/businessofferservicelocation/5
        public IHttpActionResult Get(long? id, string type)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide valid ID." });
                else
                {
                    if (type != "all")
                    {
                        var model = _db.tblBusinessOfferServiceLocations.ToList();
                        if (model != null)
                        {
                            var offerLocation = model.Select(s => new
                            {
                                Id = s.Id,
                                BusinessOfferId = s.BusinessOfferId,
                                ServiceLocationId = s.ServiceLocationId,
                                BusinessOfferViewModel = _db.tblBusinessOffers.Find(s.BusinessOfferId),
                                ServiceLocationViewModel = _db.tblServiceLocations.Find(s.ServiceLocationId)
                            });
                            return Ok(new { status = true, data = offerLocation, message = "success" });
                        }
                        else
                            return Ok(new { status = false, data = "", message = "Not found." });
                    }
                    else
                    {
                        var offerLocations = _db.tblBusinessOfferServiceLocations.Where(d => d.BusinessOfferId == id).ToList();
                        if (offerLocations.Count > 0)
                        {
                            var locations = offerLocations.Select(s => new
                            {
                                Id = s.Id,
                                BusinessOfferId = s.BusinessOfferId,
                                ServiceLocationId = s.ServiceLocationId,
                                BusinessOfferViewModel = _db.tblBusinessOffers.Find(s.BusinessOfferId),
                                ServiceLocationViewModel = _db.tblServiceLocations.Find(s.ServiceLocationId)
                            });
                            return Ok(new { status = true, data = locations, message = "success" });
                        }
                        return Ok(new { status = false, data = "", message = "No records." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // GET: api/businessofferservicelocation/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide valid ID." });
                else
                {
                    var model = _db.tblBusinessOfferServiceLocations.Find(id);
                    if (model != null)
                    {
                        var offerLocation = new
                        {
                            Id = model.Id,
                            BusinessOfferId = model.BusinessOfferId,
                            ServiceLocationId = model.ServiceLocationId,
                            BusinessOfferViewModel = _db.tblBusinessOffers.Find(model.BusinessOfferId),
                            ServiceLocationViewModel = _db.tblServiceLocations.Find(model.ServiceLocationId)
                        };
                        return Ok(new { status = true, data = offerLocation, message = "success" });
                    }
                    else
                        return Ok(new { status = false, data = "", message = "No found." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // POST: api/businessofferservicelocation
        public IHttpActionResult Post([FromBody]BusinessOfferServiceLocationViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var businessOfferLocation = new tblBusinessOfferServiceLocation()
                    {
                        BusinessOfferId = model.BusinessOfferId,
                        ServiceLocationId = model.ServiceLocationId
                    };
                    var check = _db.tblBusinessOfferServiceLocations.Where(d => d.BusinessOfferId == model.BusinessOfferId && d.ServiceLocationId == model.ServiceLocationId).ToList();
                    if (check.Count <= 0)
                    {
                        _db.tblBusinessOfferServiceLocations.Add(businessOfferLocation);
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessOfferLocation, message = "success" });
                        else
                            return Ok(new { status = false, data = "", message = "There was a problem." });
                    }
                    else
                    {
                        return Ok(new { status = false, data = "", message = "Offer has already linked with this offer. Try to link another location." });
                    }
                }
                else
                {
                    return Ok(new { status = false, data = "", message = "There was a problem." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // PUT: api/businessoffer/5
        public IHttpActionResult Put(long? id, [FromBody]BusinessOfferServiceLocationViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid ID." });
                if (model != null)
                {
                    var businessOfferLocation = _db.tblBusinessOfferServiceLocations.Find(id.Value);
                    if (businessOfferLocation != null)
                    {
                        businessOfferLocation.BusinessOfferId = model.BusinessOfferId;
                        businessOfferLocation.ServiceLocationId = model.ServiceLocationId;

                        _db.Entry(businessOfferLocation).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessOfferLocation, message = "success" });
                        else
                            return Ok(new { status = false, data = "", message = "There was a problem to update the data." });
                    }
                }
                return Ok(new { status = false, data = "", message = "Not a valid data to update. Please provide a valid id." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // DELETE: api/businessoffer/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid ID." });
                else
                {
                    var businessOfferLocation = _db.tblBusinessOfferServiceLocations.Find(id);
                    if (businessOfferLocation != null)
                    {
                        _db.Entry(businessOfferLocation).State = EntityState.Deleted;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessOfferLocation, message = "success" });
                        else
                            return Ok(new { status = false, data = "", message = "There was a problem to update the data." });
                    }
                    else
                    {
                        return Ok(new { status = false, data = "", message = "Not a valid data to update. Please provide a valid id." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }
    }
}
