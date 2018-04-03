using System;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class CustomerController : CustomerBaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index(int? page, string search)
        {
            var model = this.ResponseHelper.GetResponse<IPagedList<BusinessCustomerViewMdoel>>();
            var pageNumber = page ?? 1;
            ViewBag.search = search;

            ViewBag.BusinessId = RegisterViewModel.Business.Id;
            ViewBag.ServiceLocationId = RegisterViewModel.Employee.ServiceLocationId;

            var result = await BusinessCustomerService.Gets(RegisterViewModel.Business.Id, TableType.BusinessId);
            if (result.Status)
            {
                var data = result.Data;
                model.Status = result.Status;
                model.Message = result.Message;
                if (search == null)
                {
                    model.Data = data.ToPagedList<BusinessCustomerViewMdoel>(pageNumber, 5);
                }
                else
                {
                    model.Data = data.Where(d => d.FirstName.ToLower().Contains(search.ToLower()) || d.LastName.ToLower().Contains(search.ToLower())).ToList().ToPagedList(pageNumber, 5);
                }
            }
            else
            {
                model.Status = false;
                model.Message = result.Message;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            var response = this.ResponseHelper.GetResponse<BusinessCustomerViewMdoel>();
            response.Data = new BusinessCustomerViewMdoel();
            response.Data.ServiceLocationId = RegisterViewModel.Employee.ServiceLocationId;
            response.Status = true;

            var ServiceLocations = await this.GetServiceLocations();
            ViewBag.ServiceLocationId = ServiceLocations.Select(s => new SelectListItem()
            {
                Value = Convert.ToString(s.Id),
                Text = s.Name
            });

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "Data")]ResponseViewModel<BusinessCustomerViewMdoel> model)
        {
            var result = new ResponseViewModel<BusinessCustomerViewMdoel>();

            if (!ModelState.IsValid)
            {
                var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                result.Status = false;
                result.Message = errMessage;
            }
            else
            {
                var response = await this.BusinessCustomerService.Add(model.Data);
                if (response == null)
                {
                    result.Status = false;
                    result.Message = response != null ? response.Message : "There was a problem. Please try again later.";
                }
                else
                {
                    result.Status = response.Status;
                    result.Message = response.Message;
                }
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long? id, long? locationid)
        {
            if (id.HasValue && locationid.HasValue)
            {
                ViewBag.CustomerId = id.Value;

                var response = await this.BusinessCustomerService.Get(id.Value);

                if (response != null)
                {
                    if (response.Status)
                    {
                        var ServiceLocations = await this.GetServiceLocations();
                        ViewBag.ServiceLocationId = ServiceLocations.Select(s => new SelectListItem()
                        {
                            Value = Convert.ToString(s.Id),
                            Text = s.Name,
                            Selected = s.Id == locationid.Value ? true : false
                        });

                        response.Status = true;
                        return View(response);
                    }

                }
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Data")]ResponseViewModel<BusinessCustomerViewMdoel> model)
        {
            var result = new ResponseViewModel<BusinessCustomerViewMdoel>();
            var response = await this.BusinessCustomerService.Update(model.Data);
            if (response == null)
            {
                result.Status = false;
                result.Message = response != null ? response.Message : "There was a problem. Please try again later.";
            }
            else
            {
                result.Status = response.Status;
                result.Message = response.Message;
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Deactive(long? id)
        {
            if (id.HasValue)
            {
                var response = await this.BusinessCustomerService.Get(id.Value);

                if (response != null)
                {
                    if (response.Status)
                    {
                        response.Status = true;
                        return View(response);
                    }

                }
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deactive([Bind(Include = "Data")]ResponseViewModel<BusinessCustomerViewMdoel> model)
        {
            var result = new ResponseViewModel<BusinessCustomerViewMdoel>();
            var response = await this.BusinessCustomerService.Deactive(model.Data.Id, model.Data.IsActive);
            if (response == null)
            {
                result.Status = false;
                result.Message = response != null ? response.Message : "There was a problem. Please try again later.";
            }
            else
            {
                result.Status = response.Status;
                result.Message = response.Message;
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id.HasValue)
            {
                var response = await this.BusinessCustomerService.Get(id.Value);

                if (response != null)
                {
                    if (response.Status)
                    {
                        response.Status = true;
                        return View(response);
                    }

                }
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind(Include = "Data")]ResponseViewModel<BusinessCustomerViewMdoel> model)
        {
            var result = new ResponseViewModel<BusinessCustomerViewMdoel>();
            var response = await this.BusinessCustomerService.Delete(model.Data.Id);
            if (response == null)
            {
                result.Status = false;
                result.Message = response != null ? response.Message : "There was a problem. Please try again later.";
            }
            else
            {
                result.Status = response.Status;
                result.Message = response.Message;
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }


        [NonAction]
        private async Task<List<ServiceLocationViewModel>> GetServiceLocations()
        {
            var response = await this.ServiceLocationService.Gets(RegisterViewModel.Business.Id, TableType.BusinessId);
            if (response != null)
            {
                return response.Data;
            }
            else
            {
                return new List<ServiceLocationViewModel>();
            }
        }
    }
}