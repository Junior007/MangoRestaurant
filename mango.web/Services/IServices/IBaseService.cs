using mango.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mango.web.Services.IServices
{
    public interface IBaseService: IDisposable
    {
        //ResponseDto<A> responseModel { get; set; }
        Task<ResponseDto<B>> SendAsync<B>(ApiRequest apiRequest);
    }
}
