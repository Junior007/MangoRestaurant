using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mango.product.application.Models
{
    public class ResponseDto<T>
    {
        public bool IsSuccess { get; set; } = true;
        public T Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string> ErrorMessages { get; set; }

    }
}
