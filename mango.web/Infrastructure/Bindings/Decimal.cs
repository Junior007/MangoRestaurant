using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace mango.web.Infrastructure.Bindings
{
    public class Decimal : FloatingPointBinder<decimal>
    {
        protected override Func<string, IFormatProvider, double> ConvertFunc => Convert.ToDecimal;
    }

    //public class Double : IModelBinder
    //{
    //    public Task BindModelAsync(ModelBindingContext bindingContext)
    //    {


    //        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);


    //        var valueAsString = valueProviderResult.FirstValue;

    //        double result = Convert.ToDouble(valueAsString, CultureInfo.InvariantCulture);

    //        bindingContext.Result = ModelBindingResult.Success(result);

    //        return Task.CompletedTask;
    //    }
    //}
}
