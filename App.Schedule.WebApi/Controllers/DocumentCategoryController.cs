using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Domains;
using App.Schedule.Context;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    public class DocumentCategoryController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public DocumentCategoryController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/DocumentCategory
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblDocumentCategories.ToList();
                return Ok(new { status = true, data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/DocumentCategory/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide valid ID." });
                else
                {
                    var model = _db.tblDocumentCategories.Find(id);
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

        // POST: api/DocumentCategory
        public IHttpActionResult Post([FromBody]DocumentCategoryViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var documentCategory = new tblDocumentCategory()
                    {
                        Created = model.Created.ToUniversalTime(),
                        IsActive = model.IsActive,
                        IsParent = model.IsParent,
                        Name = model.Name,
                        OrderNo = model.OrderNo,
                        ParentId = model.ParentId,
                        PictureLink = model.PictureLink,
                        Type = model.Type
                    };
                    _db.tblDocumentCategories.Add(documentCategory);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = documentCategory });
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

        // PUT: api/DocumentCategory/5
        public IHttpActionResult Put(long? id, [FromBody]DocumentCategoryViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    if (model != null)
                    {
                        var documentCategory = _db.tblDocumentCategories.Find(id);
                        if (documentCategory != null)
                        {
                            documentCategory.Created = model.Created.ToUniversalTime();
                            documentCategory.IsActive = model.IsActive;
                            documentCategory.IsParent = model.IsParent;
                            documentCategory.Name = model.Name;
                            documentCategory.OrderNo = model.OrderNo;
                            documentCategory.ParentId = model.ParentId;
                            documentCategory.PictureLink = model.PictureLink;
                            documentCategory.Type = model.Type;

                            _db.Entry(documentCategory).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = documentCategory });
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

        // DELETE: api/DocumentCategory/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var documentCategory = _db.tblDocumentCategories.Find(id);
                    if (documentCategory != null)
                    {
                        _db.tblDocumentCategories.Remove(documentCategory);
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = documentCategory });
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
