using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace mango.web.Models
{
    public class ResponseDto<T>
    {
        public bool IsSuccess { get; internal set; } = true;
        public T Result { get; internal set; }
        public string ErrorMessages { get; internal set; }
        public string StatusCode { get; internal set; }
    }
}
