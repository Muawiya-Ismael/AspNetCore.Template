using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MvcTemplate.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace MvcTemplate.Components.Mvc
{
    public class CompareAdapter : AttributeAdapterBase<CompareAttribute>
    {
        public CompareAdapter(CompareAttribute attribute)
            : base(attribute, null)
        {
            attribute.ErrorMessage = Validation.For("EqualTo");
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val-equalto"] = GetErrorMessage(context);
            context.Attributes["data-val-equalto-other"] = Attribute.OtherProperty;
        }
        public override String GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata);
        }
    }
}
