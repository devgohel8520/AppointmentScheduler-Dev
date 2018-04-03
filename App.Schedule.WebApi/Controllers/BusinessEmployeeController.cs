using System;
using System.Web;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Domains;
using App.Schedule.Context;
using System.Collections.Generic;
using App.Schedule.Domains.Helpers;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    public class BusinessEmployeeController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public BusinessEmployeeController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/businessemployee
        public IHttpActionResult Get(long? id, TableType type)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide valid ID." });

                if (type == TableType.BusinessId)
                {
                    var locations = _db.tblServiceLocations.Where(d => d.BusinessId == id.Value).ToList();
                    var model = new List<BusinessEmployeeViewModel>();
                    foreach (var location in locations)
                    {
                        var employees = _db.tblBusinessEmployees.Where(d => d.ServiceLocationId == location.Id).Select(s => new BusinessEmployeeViewModel
                        {
                            Created = s.Created,
                            Email = s.Email,
                            FirstName = s.FirstName,
                            Id = s.Id,
                            IsAdmin = s.IsAdmin,
                            IsActive = s.IsActive,
                            LastName = s.LastName,
                            Password = s.Password,
                            PhoneNumber = s.PhoneNumber,
                            ServiceLocationId = s.ServiceLocationId,
                            STD = s.STD,
                            ServiceLocation = new ServiceLocationViewModel() { Name = location.Name, Description = location.Description }
                        }).ToList();
                        if (employees.Count > 0)
                        {
                            foreach (var employ in employees)
                            {
                                model.Add(employ);
                            }
                        }
                    }
                    return Ok(new { status = true, data = model, message = "success" });
                }
                else
                {
                    var serviceModel = _db.tblBusinessEmployees.Where(d => d.ServiceLocationId == id.Value).ToList();
                    return Ok(new { status = true, data = serviceModel, message = "success" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // GET: api/businessemployee/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide valid ID." });
                else
                {
                    var model = _db.tblBusinessEmployees.Find(id);
                    if (model != null)
                        return Ok(new { status = true, data = model, message = "success" });
                    else
                        return Ok(new { status = false, data = "", message = "Not found" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // GET: api/businessemployee/?emailid=value&password=value
        public IHttpActionResult Get(string email, string password)
        {
            try
            {
                var loginSession = new LoginSessionViewModel();
                password = HttpContext.Current.Server.UrlDecode(password);
                var pass = Security.Encrypt(password, true);
                loginSession.Employee = _db.tblBusinessEmployees.Where(d => d.Email.ToLower() == email.ToLower() && d.Password
                == pass && d.IsActive == true).FirstOrDefault();
                if (loginSession.Employee != null)
                {
                    loginSession.Employee.Password = "";
                    var serviceLocation = _db.tblServiceLocations.Find(loginSession.Employee.ServiceLocationId);
                    if (serviceLocation != null)
                    {
                        loginSession.Business = _db.tblBusinesses.Find(serviceLocation.BusinessId);
                        return Ok(new { status = true, data = loginSession, message = "Valid credential" });
                    }
                    else
                    {
                        return Ok(new { status = false, data = "", message = "Not a valid credential" });
                    }
                }
                else
                {
                    return Ok(new { status = false, data = "", message = "Not a valid credential" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // POST: api/businessemployee
        public IHttpActionResult Post([FromBody]BusinessEmployeeViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var check = _db.tblBusinessEmployees.Any(d => d.Email.ToLower() == model.Email.ToLower() && model.ServiceLocationId == model.ServiceLocationId);
                    if (!check)
                    {
                        var businessEmployee = new tblBusinessEmployee()
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Password = Security.Encrypt(model.Password, true),
                            Email = model.Email,
                            STD = model.STD,
                            PhoneNumber = model.PhoneNumber,
                            ServiceLocationId = model.ServiceLocationId,
                            IsAdmin = model.IsAdmin,
                            Created = DateTime.Now.ToUniversalTime(),
                            IsActive = model.IsActive
                        };
                        _db.tblBusinessEmployees.Add(businessEmployee);
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessEmployee, message = "success" });
                        else
                            return Ok(new { status = false, data = "", message = "There was a problem." });
                    }
                    else
                    {
                        return Ok(new { status = false, data = "", message = "Email id has already been taken, please try another email id with same service location." });
                    }
                }
                else
                {
                    return Ok(new { status = false, data = "", message = "Model is not valid." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // PUT: api/businessemployee/5
        public IHttpActionResult Put(long? id, [FromBody]BusinessEmployeeViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid id." });
                else
                {
                    var businessEmployee = _db.tblBusinessEmployees.Find(id);
                    if (businessEmployee != null)
                    {
                        if (businessEmployee.Email.ToLower() == model.Email.ToLower())
                        {
                            businessEmployee.FirstName = model.FirstName;
                            businessEmployee.LastName = model.LastName;
                            businessEmployee.STD = model.STD;
                            businessEmployee.PhoneNumber = model.PhoneNumber;
                            businessEmployee.ServiceLocationId = model.ServiceLocationId;
                            _db.Entry(businessEmployee).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = businessEmployee, message = "success" });
                            else
                                return Ok(new { status = false, data = "", message = "There was a problem to update the data." });
                        }
                        else
                        {
                            return Ok(new { status = false, data = "", message = "Please provide a valid id to update." });
                        }
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

        // DELETE: api/businessemployee/5
        public IHttpActionResult Delete(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid ID." });
                else
                {
                    var businessEmployee = _db.tblBusinessEmployees.Find(id);
                    if (businessEmployee != null)
                    {
                        _db.Entry(businessEmployee).State = EntityState.Deleted;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessEmployee, message = "success" });
                        else
                            return Ok(new { status = false, data = "", message = "There was a problem to delete the data." });
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

        // DELETE: api/businessemployee/5
        [HttpDelete]
        public IHttpActionResult Deactive(long? id, bool? status)
        {
            try
            {
                if (!id.HasValue && status.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid ID and status." });
                else
                {
                    var businessEmployee = _db.tblBusinessEmployees.Find(id);
                    if (businessEmployee != null)
                    {
                        businessEmployee.IsActive = !businessEmployee.IsActive;
                        _db.Entry(businessEmployee).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessEmployee, message = "success" });
                        else
                            return Ok(new { status = false, data = "", message = "There was a problem to deactive the data." });
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

        [NonAction]
        [AllowAnonymous]
        public ResponseViewModel<BusinessEmployeeViewModel> Register(BusinessEmployeeViewModel model)
        {
            var data = new ResponseViewModel<BusinessEmployeeViewModel>();
            var hasEmail = _db.tblBusinessEmployees.Any(d => d.Email.ToLower() == model.Email.ToLower() && d.ServiceLocationId == model.ServiceLocationId);
            if (hasEmail)
            {
                data.Status = false;
                data.Message = "This business email has been taken. Please try another email id.";
            }
            else
            {
                var businessEmployee = new tblBusinessEmployee()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = Security.Encrypt(model.Password, true),
                    Email = model.Email,
                    STD = model.STD,
                    PhoneNumber = model.PhoneNumber,
                    ServiceLocationId = model.ServiceLocationId,
                    IsAdmin = model.IsAdmin,
                    Created = DateTime.Now.ToUniversalTime(),
                    IsActive = model.IsActive
                };
                _db.tblBusinessEmployees.Add(businessEmployee);
                var response = _db.SaveChanges();
                data.Message = response > 0 ? "success" : "failed";
                data.Status = response > 0 ? true : false;
                data.Data = model;
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
                        data.Data = model;
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
        public ResponseViewModel<BusinessEmployeeViewModel> UpdateEmployee(BusinessEmployeeViewModel model)
        {
            var data = new ResponseViewModel<BusinessEmployeeViewModel>();
            try
            {
                var businessEmployee = _db.tblBusinessEmployees.Find(model.Id);
                if (businessEmployee != null)
                {
                    var password = Security.Decrypt(businessEmployee.Password, true);
                    if (password == model.OldPassword)
                    {
                        if (businessEmployee.Email.ToLower() == model.Email.ToLower())
                        {
                            businessEmployee.Password = Security.Encrypt(model.Password, true);
                            _db.Entry(businessEmployee).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            data.Message = response > 0 ? "success" : "failed";
                            data.Status = response > 0 ? true : false;
                        }
                        else
                        {
                            data.Message = "Please enter a valid email id.";
                        }
                    }
                    else
                    {
                        data.Message = "Please enter your valid old password.";
                    }
                }
                else
                {
                    data.Message = "It is not a valid information.";
                }
            }
            catch (Exception ex)
            {
                data.Message = "ex: " + ex.Message.ToString();
            }
            return data;
        }

        [NonAction]
        public ResponseViewModel<BusinessEmployeeViewModel> DeleteEmployee(long? id)
        {
            var data = new ResponseViewModel<BusinessEmployeeViewModel>()
            {
                Status = false
            };
            try
            {
                if (id.HasValue)
                {
                    var businessEmployee = _db.tblBusinessEmployees.Find(id);
                    if (businessEmployee != null)
                    {
                        _db.Entry(businessEmployee).State = EntityState.Deleted;
                        var response = _db.SaveChanges();
                        data.Data = new BusinessEmployeeViewModel() { Email = businessEmployee.Email, Id = businessEmployee.Id };
                        data.Message = response > 0 ? "success" : "failed";
                        data.Status = response > 0 ? true : false;
                    }
                    else
                    {
                        data.Message = "Not a valid data to delete. Please provide a valid id.";
                    }
                }
            }
            catch (Exception ex)
            {
                data.Message = ex.Message.ToString();
            }
            return data;
        }
    }
}
