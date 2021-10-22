using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mango.web.Models
{
    public class ResponseDto<T>
    {
        public bool IsSuccess { get; set; } = true;
        public T Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public string ErrorMessages { get; set; }
    }
}
