using System;
using PagedList;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Controllers
{
    public class BusinessCategoryController : BusinessCategoryBaseController
    {
        public async Task<ActionResult> Index(int? page, string search)
        {
            var model = new ServiceDataViewModel<IPagedList<BusinessCategoryViewModel>>();
            try
            {
                Session["HomeLink"] = "Business Category";
                var pageNumber = page ?? 1;
                ViewBag.search = search;

                var response = await this.BusinessCategoryService.Gets();
                if (response.Status)
                {
                    //var data = response.Data.Where(d => d.ParentId == null).ToList();
                    var data = response.Data.Where(d => d.ParentId == null).ToList();
                    if (search == null)
                    {
                        model.Data = data.ToPagedList<BusinessCategoryViewModel>(pageNumber, 10);
                        return View(model);
                    }
                    else
                    {
                        model.Data = data.Where(d => d.Name.ToLower().Contains(search.ToLower())).ToList().ToPagedList(pageNumber, 10);
                        return View(model);
                    }
                }
                else
                {
                    model.HasError = !response.Status;
                    model.Error = response.Message;
                }
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Error = "There was a problem. Please try again later.";
                model.HasMore = true;
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var model = new ServiceDataViewModel<BusinessCategoryViewModel>();
            var businessCategories = await this.DashboardService.GetBusinessCategories();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Data")] ServiceDataViewModel<BusinessCategoryViewModel> model)
        {
            var result = new ResponseViewModel<BusinessCategoryViewModel>();
            if (!ModelState.IsValid)
            {
                var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                result.Status = false;
                result.Message = errMessage;
            }
            else
            {
                model.Data.AdministratorId = admin.Id;
                var response = await this.BusinessCategoryService.Add(model.Data);
                if (response != null)
                {
                    result.Status = response.Status;
                    result.Message = response.Message;
                    result.Data = response.Data;
                }
                else
                {
                    result.Status = false;
                    result.Message = "There was a problem. Please try again later.";
                }
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long? id)
        {
            var model = new ServiceDataViewModel<BusinessCategoryViewModel>();
            try
            {
                model.HasError = true;
                if (!id.HasValue)
                {
                    model.Error = "Please provide a valid id.";
                }
                else
                {
                    var res = await this.BusinessCategoryService.Get(id.Value);
                    if (res.Status)
                    {
                        var businessCategories = await this.DashboardService.GetBusinessCategories();
                        model.HasError = false;
                        model.Data = res.Data;
                    }
                    else
                    {
                        model.Error = res.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Error = "There was a problem. Please try again later.";
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Data")] ServiceDataViewModel<BusinessCategoryViewModel> model)
        {
            var result = new ResponseViewModel<BusinessCategoryViewModel>();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                    result.Status = false;
                    result.Message = errMessage;
                }
                else
                {
                    model.Data.AdministratorId = admin.Id;
                    var response = await this.BusinessCategoryService.Update(model.Data);
                    if (response.Status)
                    {
                        result.Status = true;
                        result.Message = response.Message;
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = response.Message;
                    }
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        private string HandleImageUpload(Stream fileStream, string name, int size, string type)
        {
            var imageString = "";
            try
            {
                var imageBytes = new byte[fileStream.Length];
                var result = fileStream.Read(imageBytes, 0, imageBytes.Length);
                imageString = System.Text.Encoding.ASCII.GetString(imageBytes);
            }
            catch
            {
                imageString = "";
            }

            return imageString;
        }

        [HttpGet]
        public async Task<ActionResult> Deactive(int? id)
        {
            var model = new ServiceDataViewModel<BusinessCategoryViewModel>();
            try
            {
                model.HasError = true;
                if (!id.HasValue)
                {
                    model.Error = "Please provide a valid id.";
                }
                else
                {
                    var res = await this.BusinessCategoryService.Get(id.Value);
                    if (res.Status)
                    {
                        var businessCategories = await this.DashboardService.GetBusinessCategories();
                        model.HasError = false;
                        model.Data = res.Data;
                    }
                    else
                    {
                        model.Error = res.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Error = "There was a problem. Please try again later.";
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deactive([Bind(Include = "Data")] ServiceDataViewModel<BusinessCategoryViewModel> model)
        {
            var result = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                var response = await this.BusinessCategoryService.Deactive(model.Data.Id, model.Data.IsActive);
                if (response.Status)
                {
                    result.Status = true;
                    result.Message = response.Message;
                }
                else
                {
                    result.Status = false;
                    result.Message = response.Message;
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            var model = new ServiceDataViewModel<BusinessCategoryViewModel>();
            try
            {
                model.HasError = true;
                if (!id.HasValue)
                {
                    model.Error = "Please provide a valid id.";
                }
                else
                {
                    var res = await this.BusinessCategoryService.Get(id.Value);
                    if (res.Status)
                    {
                        var businessCategories = await this.DashboardService.GetBusinessCategories();
                        model.HasError = false;
                        model.Data = res.Data;
                    }
                    else
                    {
                        model.Error = res.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Error = "There was a problem. Please try again later.";
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind(Include = "Data")] ServiceDataViewModel<BusinessCategoryViewModel> model)
        {
            var result = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                var response = await this.BusinessCategoryService.Delete(model.Data.Id);
                if (response.Status)
                {
                    result.Status = true;
                    result.Message = response.Message;
                }
                else
                {
                    result.Status = false;
                    result.Message = response.Message;
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> SubIndex(int? page, string search, int? id)
        {
            var model = new ServiceDataViewModel<IPagedList<BusinessCategoryViewModel>>();
            try
            {
                Session["HomeLink"] = "Business Sub Category";
                var pageNumber = page ?? 1;
                ViewBag.search = search;

                if (id.HasValue)
                    ViewBag.ParentId = id.Value;
                else
                {
                    return RedirectToAction("Index");
                }

                var response = await BusinessCategoryService.Gets();
                if (response.Status)
                {
                    var data = response.Data.Where(d => d.ParentId != null && d.ParentId == id.Value).ToList();
                    if (search == null)
                    {
                        if (data.Count > 0)
                            model.Data = data.ToPagedList<BusinessCategoryViewModel>(pageNumber, 10);
                        return View(model);
                    }
                    else
                    {
                        model.Data = data.Where(d => d.Name.ToLower().Contains(search.ToLower())).ToList().ToPagedList(pageNumber, 5);
                        return View(model);
                    }
                }
                else
                {
                    model.HasError = !response.Status;
                    model.Error = response.Message;
                }
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Error = "There was a problem. Please try again later.";
                model.HasMore = true;
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> SubCreate(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            var model = new ServiceDataViewModel<BusinessCategoryViewModel>();
            var businessCategories = await this.DashboardService.GetBusinessCategories();
            ViewBag.ParentId = businessCategories.Where(d => d.ParentId == null && d.Id == id.Value).Select(d => new SelectListItem()
            {
                Value = Convert.ToString(d.Id),
                Text = d.Name
            });
            model.Data = new BusinessCategoryViewModel()
            {
                ParentId = id.Value
            };
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubCreate([Bind(Include = "Data")] ServiceDataViewModel<BusinessCategoryViewModel> model)
        {
            var result = new ResponseViewModel<BusinessCategoryViewModel>();
            if (!ModelState.IsValid)
            {
                var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                result.Status = false;
                result.Message = errMessage;
            }
            else
            {
                model.Data.AdministratorId = admin.Id;
                var response = await this.BusinessCategoryService.Add(model.Data);
                if (response != null)
                {
                    result.Status = response.Status;
                    result.Message = response.Message;
                    result.Data = response.Data;
                }
                else
                {
                    result.Status = false;
                    result.Message = "There was a problem. Please try again later.";
                }
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> SubEdit(long? id, int? parentId)
        {
            var model = new ServiceDataViewModel<BusinessCategoryViewModel>();
            try
            {
                model.HasError = true;
                if (!id.HasValue)
                {
                    model.Error = "Please provide a valid id.";
                }
                else
                {
                    var res = await this.BusinessCategoryService.Get(id.Value);
                    if (res.Status)
                    {
                        var businessCategories = await this.DashboardService.GetBusinessCategories();
                        ViewBag.ParentId = businessCategories.Select(d => new SelectListItem()
                        {
                            Value = Convert.ToString(d.Id),
                            Text = d.Name
                        });
                        model.HasError = false;
                        model.Data = res.Data;
                    }
                    else
                    {
                        model.Error = res.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Error = "There was a problem. Please try again later.";
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubEdit([Bind(Include = "Data")] ServiceDataViewModel<BusinessCategoryViewModel> model)
        {
            var result = new ResponseViewModel<BusinessCategoryViewModel>();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                    result.Status = false;
                    result.Message = errMessage;
                }
                else
                {
                    model.Data.AdministratorId = admin.Id;
                    var response = await this.BusinessCategoryService.Update(model.Data);
                    if (response.Status)
                    {
                        result.Status = true;
                        result.Message = response.Message;
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = response.Message;
                    }
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> SubDeactive(int? id)
        {
            var model = new ServiceDataViewModel<BusinessCategoryViewModel>();
            try
            {
                model.HasError = true;
                if (!id.HasValue)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var res = await this.BusinessCategoryService.Get(id.Value);
                    if (res.Status)
                    {
                        var businessCategories = await this.DashboardService.GetBusinessCategories();
                        model.HasError = false;
                        model.Data = res.Data;
                    }
                    else
                    {
                        model.Error = res.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Error = "There was a problem. Please try again later.";
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubDeactive([Bind(Include = "Data")] ServiceDataViewModel<BusinessCategoryViewModel> model)
        {
            var result = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                var response = await this.BusinessCategoryService.Deactive(model.Data.Id, model.Data.IsActive);
                if (response.Status)
                {
                    result.Status = true;
                    result.Message = response.Message;
                }
                else
                {
                    result.Status = false;
                    result.Message = response.Message;
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> SubDelete(int? id)
        {
            var model = new ServiceDataViewModel<BusinessCategoryViewModel>();
            try
            {
                model.HasError = true;
                if (!id.HasValue)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var res = await this.BusinessCategoryService.Get(id.Value);
                    if (res.Status)
                    {
                        var businessCategories = await this.DashboardService.GetBusinessCategories();
                        model.HasError = false;
                        model.Data = res.Data;
                    }
                    else
                    {
                        model.Error = res.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Error = "There was a problem. Please try again later.";
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubDelete([Bind(Include = "Data")] ServiceDataViewModel<BusinessCategoryViewModel> model)
        {
            var result = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                var response = await this.BusinessCategoryService.Delete(model.Data.Id);
                if (response.Status)
                {
                    result.Status = true;
                    result.Message = response.Message;
                }
                else
                {
                    result.Status = false;
                    result.Message = response.Message;
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}