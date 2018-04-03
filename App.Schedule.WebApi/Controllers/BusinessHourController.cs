using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Context;
using App.Schedule.Domains;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    public class BusinessHourController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public BusinessHourController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/businesshour
        public IHttpActionResult Get(long? id, TableType type)
        {
            try
            {
                var model = new List<tblBusinessHour>();
                if (type == TableType.None)
                {
                    model = _db.tblBusinessHours.ToList();
                }
                else if (type == TableType.ServiceLocationId)
                {
                    model = _db.tblBusinessHours.Where(d => d.ServiceLocationId == id).ToList();
                }
                return Ok(new { status = true, data = model, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // GET: api/businesshour/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide valid ID." });
                else
                {
                    var model = _db.tblBusinessHours.Find(id);
                    if (model != null)
                        return Ok(new { status = true, data = model, message = "success" });
                    else
                        return Ok(new { status = false, data = "", message = "Record not found." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // POST: api/businesshour
        public IHttpActionResult Post([FromBody]BusinessHourViewModel model)
        {
            try
            {
                if (model != null && model.From > DateTime.Now)
                {
                    var businessHour = new tblBusinessHour()
                    {
                        WeekDayId = model.WeekDayId,
                        IsStartDay = model.IsStartDay,
                        IsHoliday = model.IsHoliday,
                        From = model.From,
                        To = model.To,
                        IsSplit1 = model.IsSplit1,
                        FromSplit1 = model.FromSplit1,
                        ToSplit1 = model.ToSplit1,
                        IsSplit2 = model.IsSplit2,
                        FromSplit2 = model.FromSplit2,
                        ToSplit2 = model.ToSplit2,
                        ServiceLocationId = model.ServiceLocationId
                    };
                    _db.tblBusinessHours.Add(businessHour);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = businessHour, message = "success" });
                    else
                        return Ok(new { status = false, data = "", message = "There was a problem." });
                }
                else
                {
                    return Ok(new { status = false, data = "", message = "Please enter a valid information." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // PUT: api/businesshour/5
        public IHttpActionResult Put(long? id, [FromBody]BusinessHourViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid ID." });
                else
                {
                    if (model.IsStartDay)
                    {
                        var hasStartDay = _db.tblBusinessHours.Any(d => d.ServiceLocationId == model.ServiceLocationId && d.IsStartDay == true && d.Id != model.Id);
                        if (hasStartDay)
                        {
                            return Ok(new { status = false, data = "", message = "You can not set start day more than one." });
                        }
                    }

                    var businessHour = _db.tblBusinessHours.Find(id);
                    if (businessHour != null)
                    {
                        businessHour.WeekDayId = model.WeekDayId;
                        businessHour.IsStartDay = model.IsStartDay;
                        businessHour.IsHoliday = model.IsHoliday;
                        businessHour.From = model.From;
                        businessHour.To = model.To;
                        businessHour.IsSplit1 = model.IsSplit1;
                        businessHour.FromSplit1 = model.FromSplit1;
                        businessHour.ToSplit1 = model.ToSplit1;
                        businessHour.IsSplit2 = model.IsSplit2;
                        businessHour.FromSplit2 = model.FromSplit2;
                        businessHour.ToSplit2 = model.ToSplit2;
                        businessHour.ServiceLocationId = model.ServiceLocationId;

                        _db.Entry(businessHour).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessHour, message = "success" });
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

        // DELETE: api/businesshour/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid ID." });
                else
                {
                    var businessHour = _db.tblBusinessHours.Find(id);
                    if (businessHour != null)
                    {
                        _db.tblBusinessHours.Remove(businessHour);
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = "", message = "Successfully removed." });
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

        /// <summary>
        /// Set up new business default hours for weekday.
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public int SetupBusinessHours(long serviceLocationId)
        {
            try
            {
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
                        ServiceLocationId = serviceLocationId
                    };
                    _db.tblBusinessHours.Add(businessHour);
                }
                return _db.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }
    }
}
