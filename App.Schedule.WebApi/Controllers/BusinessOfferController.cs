using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    public class BusinessOfferController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public BusinessOfferController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/businessoffer
        public IHttpActionResult Get(long? id, TableType type)
        {
            try
            {
                if (type == TableType.EmployeeId)
                {
                    var businessOffers = _db.tblBusinessOffers.Where(d => d.BusinessEmployeeId == id).ToList();
                    return Ok(new { status = true, data = businessOffers, message = "success" });
                }
                else
                {
                    var model = _db.tblBusinessOffers.ToList();
                    return Ok(new { status = true, data = model, message = "success" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // GET: api/businessoffer/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data ="", message = "Please provide valid ID." });
                else
                {
                    var model = _db.tblBusinessOffers.Find(id);
                    if (model != null)
                        return Ok(new { status = true, data = model, message = "success" });
                    else
                        return Ok(new { status = false, data = "", message = "Not found." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: "+ex.Message.ToString() });
            }
        }

        // POST: api/businessoffer
        public IHttpActionResult Post([FromBody]BusinessOfferViewModel model)
        {
            try
            {
                if (model != null)
                {
                    if (model.ValidFrom > model.ValidTo)
                    {
                        return Ok(new { status = false, data = "", message = "Please provide a valid date from and to." });
                    }
                    else if (model.ValidFrom.ToUniversalTime().Year < DateTime.UtcNow.Year || model.ValidTo.ToUniversalTime().Year < DateTime.UtcNow.Year)
                    {
                        return Ok(new { status = false, data = "", message = "Please provide a valid date from and to." });
                    }
                    else
                    {
                        var businessOffer = new tblBusinessOffer()
                        {
                            BusinessEmployeeId = model.BusinessEmployeeId,
                            Code = model.Code,
                            Created = DateTime.Now.ToUniversalTime(),
                            Description = model.Description,
                            IsActive = true,
                            Name = model.Name,
                            ValidFrom = model.ValidFrom.ToUniversalTime(),
                            ValidTo = model.ValidTo.ToUniversalTime()
                        };
                        _db.tblBusinessOffers.Add(businessOffer);
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessOffer, message = "success" });
                        else
                            return Ok(new { status = false, data = "", message = "There was a problem." });
                    }
                }
                else
                {
                    return Ok(new { status = false, data ="", message = "There was a problem." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // PUT: api/businessoffer/5
        public IHttpActionResult Put(long? id, [FromBody]BusinessOfferViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data ="", message = "Please provide a valid ID." });
                else if(model.ValidFrom > model.ValidTo)
                {
                    return Ok(new { status = false, data = "", message = "Please provide a valid date from and to." });
                }
                else if(model.ValidFrom.ToUniversalTime().Year < DateTime.UtcNow.Year || model.ValidTo.ToUniversalTime().Year < DateTime.UtcNow.Year)
                {
                    return Ok(new { status = false, data = "", message = "Please provide a valid date from and to." });
                }
                else
                {
                    if (model != null)
                    {
                        var businessOffer = _db.tblBusinessOffers.Find(id.Value);
                        if (businessOffer != null)
                        {
                            businessOffer.Name = model.Name;
                            businessOffer.Code = model.Code;
                            businessOffer.Description = model.Description;
                            businessOffer.ValidFrom = model.ValidFrom.ToUniversalTime();
                            businessOffer.ValidTo = model.ValidTo.ToUniversalTime();
                            businessOffer.IsActive = true;
                            businessOffer.BusinessEmployeeId = model.BusinessEmployeeId;

                            _db.Entry(businessOffer).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = businessOffer, message = "success" });
                            else
                                return Ok(new { status = false, data = "", message = "There was a problem to update the data." });
                        }
                    }
                    return Ok(new { status = false, data ="", message = "Not a valid data to update. Please provide a valid id." });
                }
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
                    return Ok(new { status = false, data = "", message  = "Please provide a valid ID." });
                else
                {
                    var businessOffer = _db.tblBusinessOffers.Find(id);
                    if (businessOffer != null)
                    {
                        _db.Entry(businessOffer).State = EntityState.Deleted;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessOffer, message = "success" });
                        else
                            return Ok(new { status = false, data = "", message = "There was a problem to update the data." });
                    }
                    else
                    {
                        return Ok(new { status = false, data ="", message = "Not a valid data to update. Please provide a valid id." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        [HttpDelete]
        public IHttpActionResult Deactive(int? id, bool status)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid ID." });
                else
                {
                    var businessOffer = _db.tblBusinessOffers.Find(id);
                    if (businessOffer != null)
                    {
                        businessOffer.IsActive = status;
                        _db.Entry(businessOffer).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessOffer, message = "success" });
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
