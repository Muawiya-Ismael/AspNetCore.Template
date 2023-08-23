using AspNetCore.Components.Security;
using AspNetCore.Objects;
using AspNetCore.Services.Administration;
using AspNetCore.Validators.Administration;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers.Administration;

[Area(nameof(Area.Administration))]
public class Customers : ValidatedController<CustomerValidator, CustomerService>
{
    public Customers(CustomerValidator validator, CustomerService service)
        : base(validator, service)
    {
    }

    [HttpGet]
    public ViewResult Index()
    {
        return View(Service.GetViews());
    }

    [HttpGet]
    public ViewResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(CustomerCreateView customer)
    {
        if (!Validator.CanCreate(customer))
            return View(customer);

        Service.Create(customer);

        Authorization.Refresh(HttpContext.RequestServices);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [AuthorizeAs(nameof(Index))]
    public ActionResult Details(Int64 id)
    {
        return NotEmptyView(Service.Get<CustomerView>(id));
    }

    [HttpGet]
    public ActionResult Edit(Int64 id)
    {
        return NotEmptyView(Service.Get<CustomerEditView>(id));
    }

    [HttpPost]
    public ActionResult Edit(CustomerEditView customer)
    {
        if (!Validator.CanEdit(customer))
            return View(customer);

        Service.Edit(customer);

        Authorization.Refresh(HttpContext.RequestServices);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public ActionResult Delete(Int64 id)
    {
        return NotEmptyView(Service.Get<CustomerView>(id));
    }

    [HttpPost]
    [ActionName("Delete")]
    public RedirectToActionResult DeleteConfirmed(Int64 id)
    {
        Service.Delete(id);

        Authorization.Refresh(HttpContext.RequestServices);

        return RedirectToAction(nameof(Index));
    }
}
