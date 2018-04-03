using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Domains;
using App.Schedule.Context;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    public class AppointmentPaymentController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public AppointmentPaymentController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/AppointmentPayment
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblAppointmentPayments.ToList();
                return Ok(new { status = true, data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/AppointmentPayment/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide valid ID." });
                else
                {
                    var model = _db.tblAppointmentPayments.Find(id);
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

        // POST: api/AppointmentPayment
        public IHttpActionResult Post([FromBody]AppointmentPaymentViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var appointmentPayment = new tblAppointmentPayment()
                    {
                        Amount = model.Amount,
                        AppointmentId = model.AppointmentId,
                        BillingType = model.BillingType,
                        CardType = model.CardType,
                        CCardNumber = model.CCardNumber,
                        CCExpirationDate = model.CCExpirationDate,
                        CCFirstName = model.CCFirstName,
                        CCLastName = model.CCLastName,
                        CCSecurityCode = model.CCSecurityCode,
                        ChequeNumber = model.ChequeNumber,
                        Created = model.Created.ToUniversalTime(),
                        IsPaid = model.IsPaid,
                        PaidDate = model.PaidDate.ToUniversalTime(),
                        PurchaseOrderNo = model.PurchaseOrderNo
                    };
                    _db.tblAppointmentPayments.Add(appointmentPayment);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = appointmentPayment });
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

        // PUT: api/AppointmentPayment/5
        public IHttpActionResult Put(long? id, [FromBody]AppointmentPaymentViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    if (model != null)
                    {
                        var appointmentPayment = _db.tblAppointmentPayments.Find(id);
                        if (appointmentPayment != null)
                        {
                            appointmentPayment.Amount = model.Amount;
                            appointmentPayment.AppointmentId = model.AppointmentId;
                            appointmentPayment.BillingType = model.BillingType;
                            appointmentPayment.CardType = model.CardType;
                            appointmentPayment.CCardNumber = model.CCardNumber;
                            appointmentPayment.CCExpirationDate = model.CCExpirationDate;
                            appointmentPayment.CCFirstName = model.CCFirstName;
                            appointmentPayment.CCLastName = model.CCLastName;
                            appointmentPayment.CCSecurityCode = model.CCSecurityCode;
                            appointmentPayment.ChequeNumber = model.ChequeNumber;
                            appointmentPayment.Created = model.Created.ToUniversalTime();
                            appointmentPayment.IsPaid = model.IsPaid;
                            appointmentPayment.PaidDate = model.PaidDate.ToUniversalTime();
                            appointmentPayment.PurchaseOrderNo = model.PurchaseOrderNo;

                            _db.Entry(appointmentPayment).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = appointmentPayment });
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

        // DELETE: api/AppointmentPayment/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var appointmentPayment = _db.tblAppointmentPayments.Find(id);
                    if (appointmentPayment != null)
                    {
                        _db.tblAppointmentPayments.Remove(appointmentPayment);
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = "Successfully removed" });
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
