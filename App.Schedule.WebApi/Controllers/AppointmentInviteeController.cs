using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Domains;
using App.Schedule.Context;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    public class AppointmentInviteeController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public AppointmentInviteeController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/AppointmentInvitee
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblAppointmentInvitees.ToList();
                return Ok(new { status = true, data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/AppointmentInvitee/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide valid ID." });
                else
                {
                    var model = _db.tblAppointmentInvitees.Find(id);
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

        // POST: api/AppointmentInvitee
        public IHttpActionResult Post([FromBody]AppointmentInviteeViewModel model)
        {
            try
            {
                if (model != null)
                {
                    if (_db.tblAppointmentInvitees.Any(d => d.AppointmentId == model.AppointmentId && d.BusinessEmployeeId == model.BusinessEmployeeId))
                        return Ok(new { status = false, data = "It's been already scheduled. Please try with other employee." });

                    var appointmentInvitee = new tblAppointmentInvitee()
                    {
                       AppointmentId = model.AppointmentId,
                       BusinessEmployeeId = model.BusinessEmployeeId
                    };
                    _db.tblAppointmentInvitees.Add(appointmentInvitee);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = appointmentInvitee });
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

        // PUT: api/AppointmentInvitee/5
        public IHttpActionResult Put(long? id, [FromBody]AppointmentInviteeViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    if (model != null)
                    {
                        var appointmentInVitee = _db.tblAppointmentInvitees.Find(id);
                        if (appointmentInVitee != null)
                        {
                            appointmentInVitee.AppointmentId = model.AppointmentId;
                            appointmentInVitee.BusinessEmployeeId = model.BusinessEmployeeId;

                            _db.Entry(appointmentInVitee).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = appointmentInVitee });
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

        // DELETE: api/AppointmentInvitee/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var appointmentInvitee = _db.tblAppointmentInvitees.Find(id);
                    if (appointmentInvitee != null)
                    {
                        _db.tblAppointmentInvitees.Remove(appointmentInvitee);
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = appointmentInvitee });
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
