using Microsoft.AspNetCore.Mvc.ModelBinding;
using MvcTemplate.Components.Notifications;
using System;

namespace MvcTemplate.Validators
{
    public interface IValidator : IDisposable
    {
        Alerts Alerts { get; set; }
        ModelStateDictionary ModelState { get; set; }
    }
}
