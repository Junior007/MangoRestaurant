using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace mango.web.Infrastructure.Bindings
{
    public class Double : FloatingPointBinder<double>
    {
        protected override Func<string, IFormatProvider, double> ConvertFunc => Convert.ToDouble;
    }

}
