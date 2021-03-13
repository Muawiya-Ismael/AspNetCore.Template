using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace MvcTemplate.Components.Security
{
    [AllowUnauthorized]
    [ExcludeFromCodeCoverage]
    public class AllowUnauthorizedController : AuthorizeController
    {
        [HttpGet]
        public ViewResult AuthorizedAction()
        {
            return View();
        }
    }
}
