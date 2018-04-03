using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    public class BusinessCategoryController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public BusinessCategoryController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/businesscategory
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblBusinessCategories.ToList();
                return Ok(new { status = true, data = model, message = "success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // GET: api/businesscategory/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "please provide a valid business category id." });
                else
                {
                    var model = _db.tblBusinessCategories.Find(id);
                    if (model != null)
                        return Ok(new { status = true, data = model, message = "success" });
                    else
                        return Ok(new { status = false, data = model, message = "Not found." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // POST: api/businesscategory
        public IHttpActionResult Post([FromBody]BusinessCategoryViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var businessCategory = new tblBusinessCategory()
                    {
                        Created = DateTime.Now.ToUniversalTime(),
                        Description = model.Description,
                        IsActive = model.IsActive,
                        Name = model.Name,
                        OrderNumber = model.OrderNumber,
                        ParentId = model.ParentId,
                        PictureLink = model.PictureLink,
                        Type = model.Type,
                        AdministratorId = model.AdministratorId
                    };
                    _db.tblBusinessCategories.Add(businessCategory);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = businessCategory, message = "success" });
                    else
                        return Ok(new { status = false, data = "", message = "failed" });
                }
                else
                {
                    return Ok(new { status = false, data = "", message = "please provide a valid information." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // PUT: api/businesscategory/5
        public IHttpActionResult Put(long? id, [FromBody]BusinessCategoryViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "please provide a valid business category id." });
                else
                {
                    var businessCategory = _db.tblBusinessCategories.Find(id);
                    if (businessCategory != null)
                    {
                        businessCategory.Description = model.Description;
                        businessCategory.IsActive = model.IsActive;
                        businessCategory.Name = model.Name;
                        businessCategory.OrderNumber = model.OrderNumber;
                        businessCategory.ParentId = model.ParentId;
                        businessCategory.PictureLink = model.PictureLink;
                        businessCategory.Type = model.Type;
                        businessCategory.AdministratorId = model.AdministratorId;

                        _db.Entry(businessCategory).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessCategory, message = "success" });
                        else
                            return Ok(new { status = false, data = "", message = "failed" });
                    }
                    else
                    {
                        return Ok(new { status = false, data = "", message = "Not a valid data to update. Please provide a valid information." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // DELETE: api/businesscategory/5
        public IHttpActionResult Delete(int? id,bool status, DeleteType type)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "please provide a valid business category id." });
                else
                {
                    var businessCategory = _db.tblBusinessCategories.Find(id);
                    if (businessCategory != null)
                    {
                        businessCategory.IsActive = !businessCategory.IsActive;
                        if(type == DeleteType.DeleteRecord)
                        {
                            _db.Entry(businessCategory).State = EntityState.Deleted;
                        }
                        else
                        {
                            _db.Entry(businessCategory).State = EntityState.Modified;
                        }
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessCategory, message = "success" });
                        else
                            return Ok(new { status = false, data = "", message = "failed" });
                    }
                    else
                    {
                        return Ok(new { status = false, message = "Not a valid data to update. Please provide a valid id.", data = "" });
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
