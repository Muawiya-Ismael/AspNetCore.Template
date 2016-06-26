﻿using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace MvcTemplate.Components.Mvc
{
    public class DigitsAdapter : AttributeAdapterBase<DigitsAttribute>
    {
        public DigitsAdapter(DigitsAttribute attribute)
            : base(attribute, null)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-digits"] = GetErrorMessage(context);
        }
        public override String GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata);
        }
    }
}
