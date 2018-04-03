using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Domains;
using App.Schedule.Context;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    public class AppointmentDocumentController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public AppointmentDocumentController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/AppointmentDocument
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblAppointmentDocuments.ToList();
                return Ok(new { status = true, data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/AppointmentDocument/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide valid ID." });
                else
                {
                    var model = _db.tblAppointmentDocuments.Find(id);
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

        // POST: api/AppointmentDocument
        public IHttpActionResult Post([FromBody]AppointmentDocumentViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var appointmentDocument = new tblAppointmentDocument()
                    {
                       AppointmentId = model.AppointmentId,
                       DocumentCategoryId = model.DocumentCategoryId,
                       DocumentLink = model.DocumentLink,
                       DocumentType = model.DocumentType,
                       Title = model.Title,
                       IsEmployeeUpload = model.IsEmployeeUpload
                    };
                    _db.tblAppointmentDocuments.Add(appointmentDocument);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = appointmentDocument });
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

        // PUT: api/AppointmentDocument/5
        public IHttpActionResult Put(long? id, [FromBody]AppointmentDocumentViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    if (model != null)
                    {
                        var appointmentDocument = _db.tblAppointmentDocuments.Find(id);
                        if (appointmentDocument != null)
                        {
                            appointmentDocument.AppointmentId = model.AppointmentId;
                            appointmentDocument.DocumentCategoryId = model.DocumentCategoryId;
                            appointmentDocument.DocumentLink = model.DocumentLink;
                            appointmentDocument.DocumentType = model.DocumentType;
                            appointmentDocument.Title = model.Title;
                            appointmentDocument.IsEmployeeUpload = model.IsEmployeeUpload;

                            _db.Entry(appointmentDocument).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = appointmentDocument });
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

        // DELETE: api/AppointmentDocument/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var appointmentDocument = _db.tblAppointmentDocuments.Find(id);
                    if (appointmentDocument != null)
                    {
                        _db.tblAppointmentDocuments.Remove(appointmentDocument);
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = appointmentDocument });
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
