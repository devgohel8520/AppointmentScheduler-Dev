using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    public class TimezoneController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public TimezoneController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/timezone
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblTimezones.ToList();
                return Ok(new { status = true, data = model, message = "Transaction successed." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // GET: api/timezone/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid id." });
                else
                {
                    var model = _db.tblTimezones.Find(id);
                    if (model != null)
                        return Ok(new { status = true, data = model, message = "Transaction successed." });
                    else
                        return Ok(new { status = false, data = "", message = "Not found." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // POST: api/timezone
        public IHttpActionResult Post([FromBody]TimezoneViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                    return Ok(new { status = false, data = "", message = errMessage });
                }

                var isAny = _db.tblTimezones.Any(d => d.Title.ToLower() == model.Title.ToLower());
                if (isAny)
                    return Ok(new { status = false, data = "", message = "Please try another name." });

                var timeZone = new tblTimezone()
                {
                    Title = model.Title,
                    IsDST = model.IsDST,
                    UtcOffset = model.UtcOffset,
                    CountryId = model.CountryId,
                    AdministratorId = model.AdministratorId
                };
                _db.tblTimezones.Add(timeZone);
                var response = _db.SaveChanges();

                if (response > 0)
                {
                    return Ok(new { status = true, data = timeZone, message = "Transaction successed." });
                }
                return Ok(new { status = false, data = "", message = "Transaction failed." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }

        }

        // PUT: api/timezone/5
        public IHttpActionResult Put(long? id, [FromBody]TimezoneViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid id." });
                else
                {
                    var timeZone = _db.tblTimezones.Find(id);
                    if (timeZone != null)
                    {
                        timeZone.Title = model.Title;
                        timeZone.IsDST = model.IsDST;
                        timeZone.UtcOffset = model.UtcOffset;
                        timeZone.CountryId = model.CountryId;
                        timeZone.AdministratorId = model.AdministratorId;

                        _db.Entry(timeZone).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = timeZone, message = "Transaction successed." });
                        else
                            return Ok(new { status = false, data = "", message = "Transaction failed." });
                    }
                    else
                    {
                        return Ok(new { status = false, data = "", message = "Please provide a valid administrator id." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // DELETE: api/timeZone/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid id." });
                else
                {
                    var timeZone = _db.tblTimezones.Find(id);
                    if (timeZone != null)
                    {
                        _db.tblTimezones.Remove(timeZone);
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = timeZone, message = "Transaction successed." });
                        else
                            return Ok(new { status = false, data = "", message = "Transaction failed." });
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