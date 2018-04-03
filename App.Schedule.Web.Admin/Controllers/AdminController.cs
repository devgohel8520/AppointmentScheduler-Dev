using System;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Controllers
{
    public class AdminController : AdminBaseController
    {
        public async Task<ActionResult> Index(int? page, string search)
        {
            var model = new ServiceDataViewModel<IPagedList<AdministratorViewModel>>();
            try
            {
                Session["HomeLink"] = "Administrator";
                var pageNumber = page ?? 1;
                ViewBag.search = search;
                var response = await this.AdminService.Gets();
                if (response.Status)
                {
                    var data = response.Data;
                    if (search == null)
                    {
                        model.Data = data.ToPagedList<AdministratorViewModel>(pageNumber, 10);
                        return View(model);
                    }
                    else
                    {
                        model.Data = data.Where(d => d.FirstName.ToLower().Contains(search.ToLower())).ToList().ToPagedList(pageNumber, 5);
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
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ServiceDataViewModel<AdministratorViewModel>();
            model.Data = new AdministratorViewModel();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Data")] ServiceDataViewModel<AdministratorViewModel> model)
        {
            var result = new ResponseViewModel<string>();
            if (!ModelState.IsValid)
            {
                var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                result.Status = false;
                result.Message = errMessage;
            }
            else
            {
                model.Data.Created = DateTime.Now.ToUniversalTime();
                model.Data.AdministratorId = admin.Id;
                var response = await this.AdminService.Add(model.Data);
                if (response.Status)
                {
                    result.Status = true;
                    result.Message = response.Message;
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
            var model = new ServiceDataViewModel<AdministratorViewModel>();
            try
            {
                model.HasError = true;
                if (!id.HasValue)
                {
                    model.Error = "Please provide a valid id.";
                }
                else
                {
                    var res = await this.AdminService.Get(id.Value);
                    if (res.Status)
                    {
                        model.HasError = false;
                        model.Data = res.Data;
                        model.Data.Password = "";
                        model.Data.ConfirmPassword = "";
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
        public async Task<ActionResult> Edit([Bind(Include = "Data")] ServiceDataViewModel<AdministratorViewModel> model)
        {
            var result = new ResponseViewModel<string>();
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
                    model.Data.Created = DateTime.Now.ToUniversalTime();
                    model.Data.AdministratorId = admin.Id;
                    var response = await this.AdminService.Update(model.Data);
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
        public async Task<ActionResult> Delete(long? id)
        {
            var model = new ServiceDataViewModel<AdministratorViewModel>();
            try
            {
                model.HasError = true;
                if (!id.HasValue)
                {
                    model.Error = "Please provide a valid id.";
                }
                else
                {
                    var res = await this.AdminService.Get(id.Value);
                    if (res.Status)
                    {
                        if (res.Data.Email.ToLower() != this.admin.Email.ToLower())
                        {
                            model.HasError = false;
                            model.Data = res.Data;
                            model.Data.Password = "";
                            model.Data.ConfirmPassword = "";
                        }
                        else
                        {
                            model.Error = "Sorry, You cann't remove yourself.";
                            return RedirectToAction("index");
                        }
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
        public async Task<ActionResult> Delete([Bind(Include = "Data")] ServiceDataViewModel<AdministratorViewModel> model)
        {
            var result = new ResponseViewModel<string>();
            try
            {
                if (model.Data!=null)
                {
                    if (model.Data.Email.ToLower() != this.admin.Email.ToLower())
                    {
                        var response = await this.AdminService.Delete(model.Data.Id);
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
                    else
                    {
                        result.Status = false;
                        result.Message = "Sorry, You cann't remove yourself";
                    }
                }
                else
                {
                    result.Status = false;
                    result.Message = "Not a valid data.";
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
        public async Task<ActionResult> Deactive(long? id)
        {
            var model = new ServiceDataViewModel<AdministratorViewModel>();
            try
            {
                model.HasError = true;
                if (!id.HasValue)
                {
                    model.Error = "Please provide a valid id.";
                }
                else
                {
                    var res = await this.AdminService.Get(id.Value);
                    if (res.Status)
                    {
                        model.HasError = false;
                        model.Data = res.Data;
                        model.Data.Password = "";
                        model.Data.ConfirmPassword = "";
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
        public async Task<ActionResult> Deactive([Bind(Include = "Data")] ServiceDataViewModel<AdministratorViewModel> model)
        {
            var result = new ResponseViewModel<string>();
            try
            {
                if (!string.IsNullOrEmpty(model.Data.Email))
                {
                    var response = await this.AdminService.Deactive(model.Data.Id, model.Data.IsActive);
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
                else
                {
                    result.Status = false;
                    result.Message = "Please provide a valid email id.";
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