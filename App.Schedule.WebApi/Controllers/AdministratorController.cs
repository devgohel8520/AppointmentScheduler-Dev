using System;
using System.Web;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Domains;
using App.Schedule.Context;
using App.Schedule.Domains.Helpers;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    [Authorize]
    public class AdministratorController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public AdministratorController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/administrator
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblAdministrators.ToList();
                return Ok(new { status = true, data = model, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // GET: api/administrator/5
        [AllowAnonymous]
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid id." });
                else
                {
                    var model = _db.tblAdministrators.Find(id);
                    if (model != null)
                        return Ok(new { status = true, data = model, message = "success" });
                    else
                        return Ok(new { status = false, data = "", message = "not found" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // GET: api/administrator/?email=value&password=value
        [AllowAnonymous]
        public IHttpActionResult Get(string email, string password)
        {
            try
            {
                password = HttpContext.Current.Server.UrlDecode(password);
                var pass = Security.Encrypt(password, true);
                var model = _db.tblAdministrators.Where(d => d.Email.ToLower() == email.ToLower() && d.Password
                == pass && d.IsActive == true).FirstOrDefault();
                if (model != null)
                {
                    model.Password = "";
                    return Ok(new { status = true, data = model, message = "valid credential" });
                }
                else
                {
                    return Ok(new { status = false, data = model, message = "not a valid credential" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // POST: api/administrator
        public IHttpActionResult Post([FromBody]AdministratorViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                    return Ok(new { status = false, data = "", message = errMessage });
                }

                var isAny = _db.tblAdministrators.Any(d => d.Email.ToLower() == model.Email.ToLower());
                if (isAny)
                    return Ok(new { status = false, data = "", message = "please try another email id." });

                var admin = new tblAdministrator()
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = Security.Encrypt(model.Password, true),
                    IsAdmin = model.IsAdmin,
                    IsActive = model.IsActive,
                    ContactNumber = model.ContactNumber,
                    Created = DateTime.Now.ToUniversalTime(),
                    AdministratorId = model.AdministratorId,
                };

                _db.tblAdministrators.Add(admin);
                var response = _db.SaveChanges();

                if (response > 0)
                {
                    return Ok(new { status = true, data = admin, message = "success" });
                }
                return Ok(new { status = false, data = "", message = "failed" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // PUT: api/administrator/5
        public IHttpActionResult Put(long? id, [FromBody]AdministratorViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "please provide a valid id." });
                else
                {
                    var admin = _db.tblAdministrators.Find(id);
                    if (admin != null)
                    {
                        if (admin.Email.ToLower() == model.Email.ToLower())
                        {
                            admin.FirstName = model.FirstName;
                            admin.LastName = model.LastName;
                            admin.Password = Security.Encrypt(model.Password, true);
                            admin.IsAdmin = model.IsAdmin;
                            admin.IsActive = model.IsActive;
                            admin.ContactNumber = model.ContactNumber;

                            _db.Entry(admin).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = admin, message = "success" });
                            else
                                return Ok(new { status = false, data = "", message = "failed" });
                        }
                        else
                        {
                            return Ok(new { status = false, data = "", message = "please provide a valid administrator id." });
                        }
                    }
                    else
                    {
                        return Ok(new { status = false, data = "", message = "not a valid data to update. Please provide a valid id." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        public IHttpActionResult Delete(long? id, bool status, DeleteType type)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "please provide a valid admin id." });
                else
                {
                    var admin = _db.tblAdministrators.Find(id);
                    if (admin != null)
                    {
                        admin.IsActive = status;
                        if (type == DeleteType.DeleteRecord)
                        {
                            _db.Entry(admin).State = EntityState.Deleted;
                        }
                        else
                        {
                            _db.Entry(admin).State = EntityState.Modified;
                        }
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = admin, message = "success" });
                        else
                            return Ok(new { status = false, data = "", message = "failed" });
                    }
                    else
                    {
                        return Ok(new { status = false, data = "", message = "not a valid data to update. Please provide a valid id." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        [NonAction]
        [AllowAnonymous]
        public ResponseViewModel<AdministratorViewModel> RegisterAdmin(AdministratorViewModel model)
        {
            var data = new ResponseViewModel<AdministratorViewModel>();
            try
            {
                var isAny = _db.tblAdministrators.Any(d => d.Email.ToLower() == model.Email.ToLower());
                if (isAny)
                {
                    data.Message = "This email id has already been registered. Try another email id.";
                    data.Status = false;
                }
                else
                {
                    var admin = new tblAdministrator()
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Password = Security.Encrypt(model.Password, true),
                        IsAdmin = model.IsAdmin,
                        IsActive = model.IsActive,
                        ContactNumber = model.ContactNumber,
                        Created = DateTime.Now.ToUniversalTime(),
                        AdministratorId = model.AdministratorId,
                    };

                    _db.tblAdministrators.Add(admin);
                    var response = _db.SaveChanges();

                    data.Status = response > 0 ? true : false;
                    data.Message = response > 0 ? "success" : "failed";
                }
            }
            catch (Exception ex)
            {
                data.Message = "ex: " + ex.Message.ToString();
                data.Status = false;
            }
            return data;
        }

        [NonAction]
        public ResponseViewModel<AdministratorViewModel> UpdateAdmin(AdministratorViewModel model)
        {
            var data = new ResponseViewModel<AdministratorViewModel>();
            try
            {
                var admin = _db.tblAdministrators.Find(model.Id);
                if (admin != null)
                {
                    if (admin.Email.ToLower() == model.Email.ToLower())
                    {
                        admin.FirstName = model.FirstName;
                        admin.LastName = model.LastName;
                        admin.Password = Security.Encrypt(model.Password, true);
                        admin.IsAdmin = model.IsAdmin;
                        admin.IsActive = model.IsActive;
                        admin.ContactNumber = model.ContactNumber;

                        _db.Entry(admin).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        data.Status = response > 0 ? true : false;
                        data.Message = response > 0 ? "success" : "failed";
                    }
                    else
                    {
                        data.Message = "please enter a valid email id.";
                    }
                }
                else
                {
                    data.Message = "it is not a valid admin information.";
                }
            }
            catch (Exception ex)
            {
                data.Message = "ex: " + ex.Message.ToString();
            }
            return data;
        }

        [NonAction]
        public bool BusinessHourDefaultSetup(long serviceLocationId)
        {
            var result = false;
            try
            {
                var now = DateTime.Now;
                for (var i = 0; i < 7; i++)
                {
                    var businessHour = new tblBusinessHour()
                    {
                        WeekDayId = i,
                        IsStartDay = i == 1 ? true : false,
                        IsHoliday = i == 1 ? true : false,
                        From = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0, DateTimeKind.Utc),
                        To = new DateTime(now.Year, now.Month, now.Day, 18, 0, 0, DateTimeKind.Utc),
                        IsSplit1 = false,
                        FromSplit1 = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0, DateTimeKind.Utc),
                        ToSplit1 = new DateTime(now.Year, now.Month, now.Day, 18, 0, 0, DateTimeKind.Utc),
                        IsSplit2 = false,
                        FromSplit2 = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0, DateTimeKind.Utc),
                        ToSplit2 = new DateTime(now.Year, now.Month, now.Day, 18, 0, 0, DateTimeKind.Utc),
                        ServiceLocationId = serviceLocationId
                    };
                    _db.tblBusinessHours.Add(businessHour);
                }
                var response = _db.SaveChanges();
                if (response > 0)
                    result = true;
                else
                    result = false;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
