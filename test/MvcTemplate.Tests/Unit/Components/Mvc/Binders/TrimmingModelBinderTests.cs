using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MvcTemplate.Components.Mvc.Tests
{
    public class TrimmingModelBinderTests
    {
        private TrimmingModelBinder binder;
        private ModelBindingContext context;
        private RouteValueDictionary values;

        public TrimmingModelBinderTests()
        {
            binder = new TrimmingModelBinder();
            values = new RouteValueDictionary();
            context = new DefaultModelBindingContext();
            context.ModelState = new ModelStateDictionary();
            context.ValueProvider = new RouteValueProvider(BindingSource.Path, values);
            context.ModelMetadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(String));
        }

        [Fact]
        public async Task BindModelAsync_NoValue()
        {
            context.ModelName = "Test";

            await binder.BindModelAsync(context);

            ModelBindingResult actual = context.Result;
            ModelBindingResult expected = new();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public async Task BindModelAsync_Null(String value)
        {
            values["Test"] = value;
            context.ModelName = "Test";
            // ModelMetadata metadata = Substitute.For<ModelMetadata>(ModelMetadataIdentity.ForType(typeof(String)));
            // context.ModelName = nameof(AllTypesView.StringField);
            // context.ModelMetadata.ConvertEmptyStringToNull.Returns(true);
            // metadata.IsRequired.Returns(false);
            // context.ModelMetadata = metadata;

            await binder.BindModelAsync(context);

            ModelBindingResult expected = ModelBindingResult.Success(null);
            ModelBindingResult actual = context.Result;

            Assert.Equal(expected.IsModelSet, actual.IsModelSet);
            Assert.Equal(expected.Model, actual.Model);
        }

        // [Theory]
        // [InlineData(true, true, "")]
        // [InlineData(false, true, "  ")]
        // [InlineData(false, false, "  ")]
        // public async Task BindModelAsync_Empty(Boolean convertToNull, Boolean isRequired, String value)
        // {
        //     // context.ValueProvider.GetValue(nameof(AllTypesView.StringField)).Returns(new ValueProviderResult(value));
        //     // ModelMetadata metadata = Substitute.For<ModelMetadata>(ModelMetadataIdentity.ForType(typeof(String)));
        //     // metadata.ConvertEmptyStringToNull.Returns(convertToNull);
        //     // context.ModelName = nameof(AllTypesView.StringField);
        //     // metadata.IsRequired.Returns(isRequired);
        //     // context.ModelMetadata = metadata;

        //     await binder.BindModelAsync(context);

        //     ModelBindingResult expected = ModelBindingResult.Success("");
        //     ModelBindingResult actual = context.Result;

        //     Assert.Equal(expected.IsModelSet, actual.IsModelSet);
        //     Assert.Equal(expected.Model, actual.Model);
        // }

        [Fact]
        public async Task BindModelAsync_Trimmed()
        {
            values["Test"] = " Value ";
            context.ModelName = "Test";

            await binder.BindModelAsync(context);

            ModelBindingResult expected = ModelBindingResult.Success("Value");
            ModelBindingResult actual = context.Result;

            Assert.Equal(expected.IsModelSet, actual.IsModelSet);
            Assert.Equal(expected.Model, actual.Model);
        }
    }
}
