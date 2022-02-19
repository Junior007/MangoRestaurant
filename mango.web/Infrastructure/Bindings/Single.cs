using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace mango.web.Infrastructure.Bindings
{
    public class Single : FloatingPointBinder<float>
    {
        protected override Func<string, IFormatProvider, float> ConvertFunc => Convert.ToSingle;
    }


}
