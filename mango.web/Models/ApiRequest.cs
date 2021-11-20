using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace mango.web.Models
{
    public class ApiRequest
    {
        public  HttpMethod ApiType { get; set; } = HttpMethod.Get;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
