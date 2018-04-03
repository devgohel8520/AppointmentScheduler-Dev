using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    public class CountryController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public CountryController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/country
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblCountries.ToList();
                return Ok(new { status = true, data = model, message = "Transaction successed." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // GET: api/country/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid id." });
                else
                {
                    var model = _db.tblCountries.Find(id);
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

        // POST: api/country
        public IHttpActionResult Post([FromBody]CountryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                    return Ok(new { status = false, data = "", message = errMessage });
                }

                var isAny = _db.tblCountries.Any(d => d.Name.ToLower() == model.Name.ToLower());
                if (isAny)
                    return Ok(new { status = false, data = "", message = "Please try another name." });

                var country = new tblCountry()
                {
                    Name = model.Name,
                    CurrencyCode = model.CurrencyCode,
                    CurrencyName = model.CurrencyName,
                    ISO = model.ISO,
                    ISO3 = model.ISO3,
                    PhoneCode = model.PhoneCode.Value,
                    AdministratorId = model.AdministratorId,
                };

                _db.tblCountries.Add(country);
                var response = _db.SaveChanges();

                if (response > 0)
                {
                    return Ok(new { status = true, data = country, message = "Transaction successed." });
                }
                return Ok(new { status = false, data = "", message = "Transaction failed." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // PUT: api/country/5
        public IHttpActionResult Put(long? id, [FromBody]CountryViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid id." });
                else
                {
                    var country = _db.tblCountries.Find(id);
                    if (country != null)
                    {
                        country.Name = model.Name;
                        country.ISO = model.ISO;
                        country.ISO3 = model.ISO3;
                        country.PhoneCode = model.PhoneCode.Value;
                        country.CurrencyCode = model.CurrencyCode;
                        country.CurrencyName = model.CurrencyName;

                        _db.Entry(country).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = country, message = "Transaction successed." });
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

        // DELETE: api/country/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid id." });
                else
                {
                    var country = _db.tblCountries.Find(id);
                    if (country != null)
                    {
                        _db.tblCountries.Remove(country);
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = country, message = "Transaction successed." });
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
