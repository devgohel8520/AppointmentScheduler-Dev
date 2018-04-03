using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;
using System.Collections.Generic;
using App.Schedule.Domains.Helpers;

namespace App.Schedule.WebApi.Controllers
{
    public class BusinessCustomerController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public BusinessCustomerController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/BusinessCustomer
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblBusinessCustomers.ToList();
                return Ok(new { status = true, data = model, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
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
                    var model = new List<BusinessCustomerViewMdoel>();
                    foreach (var location in locations)
                    {
                        var customers = _db.tblBusinessCustomers.Where(d => d.ServiceLocationId == location.Id).Select(s => new BusinessCustomerViewMdoel
                        {
                            Created = s.Created,
                            Email = s.Email,
                            FirstName = s.FirstName,
                            Id = s.Id,
                            IsActive = s.IsActive,
                            LastName = s.LastName,
                            Password = s.Password,
                            PhoneNumber = s.PhoneNumber,
                            ServiceLocationId = s.ServiceLocationId,
                            Add1 = s.Add1,
                            Add2 = s.Add2,
                            City = s.City,
                            State = s.City,
                            ProfilePicture = s.ProfilePicture,
                            StdCode = s.StdCode,
                            Zip = s.Zip,
                            ServiceLocation = new ServiceLocationViewModel() { Name = location.Name, Description = location.Description }
                        }).ToList();
                        if (customers.Count > 0)
                        {
                            foreach (var customer in customers)
                            {
                                model.Add(customer);
                            }
                        }
                    }
                    return Ok(new { status = true, data = model, message = "success" });
                }
                else
                {
                    var serviceModel = _db.tblBusinessCustomers.Where(d => d.ServiceLocationId == id.Value).ToList();
                    return Ok(new { status = true, data = serviceModel, message = "success" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }


        // GET: api/BusinessCustomer/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide valid ID." });
                else
                {
                    var model = _db.tblBusinessCustomers.Find(id);
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

        // POST: api/BusinessCustomer
        public IHttpActionResult Post([FromBody]BusinessCustomerViewMdoel model)
        {
            try
            {
                if (model != null)
                {
                    var businessCustomer = new tblBusinessCustomer()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        ProfilePicture = model.ProfilePicture,
                        Email = model.Email,
                        StdCode = model.StdCode,
                        PhoneNumber = model.PhoneNumber,
                        Add1 = model.Add1,
                        Add2 = model.Add2,
                        City = model.City,
                        State = model.State,
                        Zip = model.Zip,
                        Password = model.Password,
                        IsActive = model.IsActive,
                        Created = model.Created,
                        ServiceLocationId = model.ServiceLocationId
                    };
                    _db.tblBusinessCustomers.Add(businessCustomer);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = businessCustomer, message = "" });
                    else
                        return Ok(new { status = false, data = "", message = "There was a problem." });
                }
                else
                {
                    return Ok(new { status = false, data = "", message = "There was a problem." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // PUT: api/BusinessCustomer/5,
        public IHttpActionResult Put(long? id, [FromBody]BusinessCustomerViewMdoel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid ID." });
                else
                {
                    if (model != null)
                    {
                        var businessCustomer = _db.tblBusinessCustomers.Find(id);
                        if (businessCustomer != null)
                        {
                            businessCustomer.FirstName = model.FirstName;
                            businessCustomer.LastName = model.LastName;
                            businessCustomer.ProfilePicture = model.ProfilePicture;
                            businessCustomer.Email = model.Email;
                            businessCustomer.StdCode = model.StdCode;
                            businessCustomer.PhoneNumber = model.PhoneNumber;
                            businessCustomer.Add1 = model.Add1;
                            businessCustomer.Add2 = model.Add2;
                            businessCustomer.City = model.City;
                            businessCustomer.State = model.State;
                            businessCustomer.Zip = model.Zip;
                            businessCustomer.ServiceLocationId = model.ServiceLocationId;
                            _db.Entry(businessCustomer).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = businessCustomer, message = "" });
                            else
                                return Ok(new { status = false, data = "", message = "There was a problem to update the data." });
                        }
                    }
                    return Ok(new { status = false, data = "Not a valid data to update. Please provide a valid id." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // DELETE: api/BusinessCustomer/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid ID." });
                else
                {
                    var businessCustomer = _db.tblBusinessCustomers.Find(id);
                    if (businessCustomer != null)
                    {
                        businessCustomer.IsActive = !businessCustomer.IsActive;
                        _db.Entry(businessCustomer).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessCustomer, message = "" });
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

        // DELETE: api/businessemployee/5
        [HttpDelete]
        public IHttpActionResult Deactive(int? id, bool? status)
        {
            try
            {
                if (!id.HasValue && status.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid ID and status." });
                else
                {
                    var businessCustomer = _db.tblBusinessCustomers.Find(id);
                    if (businessCustomer != null)
                    {
                        businessCustomer.IsActive = !businessCustomer.IsActive;
                        _db.Entry(businessCustomer).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessCustomer, message = "success" });
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
        public ResponseViewModel<BusinessCustomerViewMdoel> Register(BusinessCustomerViewMdoel model)
        {
            var data = new ResponseViewModel<BusinessCustomerViewMdoel>();
            var hasEmail = _db.tblBusinessCustomers.Any(d => d.Email.ToLower() == model.Email.ToLower() && d.ServiceLocationId == model.ServiceLocationId);
            if (hasEmail)
            {
                data.Status = false;
                data.Message = "This business email has been taken. Please try another email id.";
            }
            else
            {
                var businessCustomer = new tblBusinessCustomer()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = Security.Encrypt(model.Password, true),
                    Email = model.Email,
                    StdCode = model.StdCode,
                    IsActive = model.IsActive,
                    PhoneNumber = model.PhoneNumber,
                    Add1 = model.Add1,
                    Add2 = model.Add2,
                    City = model.City,
                    ProfilePicture = model.ProfilePicture,
                    State = model.State,
                    Zip = model.Zip,
                    Created = DateTime.Now.ToUniversalTime(),
                    ServiceLocationId = model.ServiceLocationId
                };
                _db.tblBusinessCustomers.Add(businessCustomer);
                var response = _db.SaveChanges();
                data.Message = response > 0 ? "success" : "failed";
                data.Status = response > 0 ? true : false;
                data.Data = model;
            }
            return data;
        }

        [NonAction]
        public ResponseViewModel<BusinessCustomerViewMdoel> DeleteCustomer(long? id)
        {
            var data = new ResponseViewModel<BusinessCustomerViewMdoel>();
            try
            {
                if (id.HasValue)
                {
                    var businessCustomer = _db.tblBusinessCustomers.Find(id);
                    if (businessCustomer != null)
                    {
                        _db.Entry(businessCustomer).State = EntityState.Deleted;
                        var response = _db.SaveChanges();
                        data.Data = new BusinessCustomerViewMdoel() { Email = businessCustomer.Email, Id = businessCustomer.Id };
                        data.Message = response > 0 ? "success" : "failed";
                        data.Status = response > 0 ? true : false;
                    }
                    else
                    {
                        data.Message = "Not a valid data to update. Please provide a valid id.";
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
