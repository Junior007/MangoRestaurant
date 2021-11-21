using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace mango.web.Infrastructure.Bindings
{
    public abstract class FloatingPointBinder<T> : IModelBinder
    {

        protected abstract Func<string, IFormatProvider, T> ConvertFunc { get; }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            try
            {
                bindingContext.Result = ModelBindingResult.Success(ConvertFunc.Invoke(valueProviderResult.FirstValue, CultureInfo.CurrentUICulture));
            }
            catch (FormatException)
            {
                // If format error then fallback to InvariantCulture instead of current UI culture
                bindingContext.Result = ModelBindingResult.Success(ConvertFunc.Invoke(valueProviderResult.FirstValue, CultureInfo.InvariantCulture));
            }

            return Task.CompletedTask;
        }
    }
}