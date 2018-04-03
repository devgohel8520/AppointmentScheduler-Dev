using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace App.Schedule.WebApi.Controllers
{
    public class BusinessServiceController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public BusinessServiceController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/businessservice
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblBusinessServices.ToList();
                return Ok(new { status = true, data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/businessservice/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide valid ID." });
                else
                {
                    var model = _db.tblBusinessServices.Find(id);
                    if (model != null)
                        return Ok(new { status = true, data = model });
                    else
                        return Ok(new { status = false, data = "Not found." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // POST: api/businessservice
        public IHttpActionResult Post([FromBody]BusinessServiceViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var businessService = new tblBusinessService()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Cost = model.Cost,
                        IsActive = model.IsActive,
                        Created = model.Created,
                        EmployeeId = model.EmployeeId
                    };
                    _db.tblBusinessServices.Add(businessService);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = businessService });
                    else
                        return Ok(new { status = false, data = "There was a problem." });
                }
                else
                {
                    return Ok(new { status = false, data = "There was a problem." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // PUT: api/businessservice/5
        public IHttpActionResult Put(long? id, [FromBody]BusinessServiceViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    if (model != null)
                    {
                        var businessService = _db.tblBusinessServices.Find(id);
                        if (businessService != null)
                        {
                            businessService.Name = model.Name;
                            businessService.Description = model.Description;
                            businessService.Cost = model.Cost;
                            businessService.IsActive = model.IsActive;
                            businessService.Created = model.Created;
                            businessService.EmployeeId = model.EmployeeId;

                            _db.Entry(businessService).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = businessService });
                            else
                                return Ok(new { status = false, data = "There was a problem to update the data." });
                        }
                    }
                    return Ok(new { status = false, data = "Not a valid data to update. Please provide a valid id." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // DELETE: api/businessservice/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var businessService = _db.tblBusinessServices.Find(id);
                    if (businessService != null)
                    {
                        businessService.IsActive = !businessService.IsActive;
                        _db.Entry(businessService).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessService });
                        else
                            return Ok(new { status = false, data = "There was a problem to update the data." });
                    }
                    else
                    {
                        return Ok(new { status = false, data = "Not a valid data to update. Please provide a valid id." });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
