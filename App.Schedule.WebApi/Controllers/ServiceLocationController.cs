using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    public class ServiceLocationController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public ServiceLocationController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/servicelocation
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblServiceLocations.ToList();
                return Ok(new { status = true, data = model, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        public IHttpActionResult Get(long?id, TableType type)
        {
            try
            {
                if (type == TableType.BusinessId)
                {
                    var model = _db.tblServiceLocations.Where(d => d.BusinessId == id.Value).ToList();
                    return Ok(new { status = true, data = model, message = "success" });
                }
                else
                {
                    var model = _db.tblServiceLocations.ToList();
                    return Ok(new { status = true, data = model, message = "success" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }


        // GET: api/servicelocation/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, message = "Please provide valid ID." });
                else
                {
                    var model = _db.tblServiceLocations.Find(id);
                    if (model != null)
                        return Ok(new { status = true, data = model, message = "success" });
                    else
                        return Ok(new { status = false, data = "", message = "Not found." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // POST: api/servicelocation
        public IHttpActionResult Post([FromBody]ServiceLocationViewModel model)
        {
            try
            {
                var serviceLocation = new tblServiceLocation()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Add1 = model.Add1,
                    Add2 = model.Add2,
                    City = model.City,
                    State = model.State,
                    Zip = model.Zip,
                    CountryId = model.CountryId,
                    Created = DateTime.Now.ToUniversalTime(),
                    IsActive = model.IsActive,
                    BusinessId = model.BusinessId,
                    TimezoneId = model.TimezoneId
                };

                using (var dbTrans = _db.Database.BeginTransaction())
                {
                    _db.tblServiceLocations.Add(serviceLocation);
                    var responseLocation = _db.SaveChanges();

                    var today = DateTime.Now;
                    var date = new DateTime(today.Year, today.Month, today.Day, 8, 00, 00, DateTimeKind.Utc);
                    for (int i = 0; i < 7; i++)
                    {
                        var businessHour = new tblBusinessHour()
                        {
                            WeekDayId = i,
                            IsStartDay = i == 0 ? true : false,
                            IsHoliday = false,
                            From = date,
                            To = date.AddHours(10),
                            IsSplit1 = false,
                            FromSplit1 = null,
                            ToSplit1 = null,
                            IsSplit2 = false,
                            FromSplit2 = null,
                            ToSplit2 = null,
                            ServiceLocationId = serviceLocation.Id
                        };
                        _db.tblBusinessHours.Add(businessHour);
                    }
                    var responseHour = _db.SaveChanges();
                    if (responseHour > 0 && responseLocation > 0)
                    {
                        dbTrans.Commit();
                        return Ok(new { status = true, data = serviceLocation, message = "success" });

                    }
                    else
                    {
                        dbTrans.Rollback();
                        return Ok(new { status = false, data = "", message = "There was a problem." });

                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // PUT: api/servicelocation/5
        public IHttpActionResult Put(long? id, [FromBody]ServiceLocationViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid ID." });
                else
                {
                    var serviceLocation = _db.tblServiceLocations.Find(id);
                    if (serviceLocation != null)
                    {
                        serviceLocation.Name = model.Name;
                        serviceLocation.Description = model.Description;
                        serviceLocation.Add1 = model.Add1;
                        serviceLocation.Add2 = model.Add2;
                        serviceLocation.City = model.City;
                        serviceLocation.State = model.State;
                        serviceLocation.Zip = model.Zip;
                        serviceLocation.CountryId = model.CountryId;
                        serviceLocation.Created = DateTime.Now.ToUniversalTime();
                        serviceLocation.IsActive = model.IsActive;
                        serviceLocation.BusinessId = model.BusinessId;
                        serviceLocation.TimezoneId = model.TimezoneId;

                        _db.Entry(serviceLocation).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = serviceLocation, message = "success" });
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
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // DELETE: api/servicelocation/5
        public IHttpActionResult Delete(int? id, DeleteType type)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid ID." });
                else
                {
                    var serviceLocation = _db.tblServiceLocations.Find(id);
                    if (serviceLocation != null)
                    {
                        if (type == DeleteType.DeleteRecord)
                        {
                            _db.Entry(serviceLocation).State = EntityState.Deleted;
                        }
                        else
                        {
                            serviceLocation.IsActive = !serviceLocation.IsActive;
                            _db.Entry(serviceLocation).State = EntityState.Modified;
                        }
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = serviceLocation, message = "success" });
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
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }
    }
}
