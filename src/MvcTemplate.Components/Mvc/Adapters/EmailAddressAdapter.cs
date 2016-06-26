﻿using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MvcTemplate.Resources.Form;
using System;
using System.ComponentModel.DataAnnotations;

namespace MvcTemplate.Components.Mvc
{
    public class EmailAddressAdapter : AttributeAdapterBase<EmailAddressAttribute>
    {
        public EmailAddressAdapter(EmailAddressAttribute attribute)
            : base(attribute, null)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-email"] = GetErrorMessage(context);
        }
        public override String GetErrorMessage(ModelValidationContextBase validationContext)
        {
            Attribute.ErrorMessage = Validations.Email;

            return GetErrorMessage(validationContext.ModelMetadata);
        }
    }
}
