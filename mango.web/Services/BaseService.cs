using mango.web.Models;
using mango.web.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace mango.web.Services
{
    public class BaseService : IBaseService
    {
        //public ResponseDto responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        private readonly ILogger<BaseService> _logger;
        public BaseService(IHttpClientFactory httpClient, ILogger<BaseService> logger)
        {
            this.httpClient = httpClient;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<ResponseDto<T>> SendAsync<T>(ApiRequest apiRequest)
        {

            try
            {
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                var client = httpClient.CreateClient("MangoAPI");
                client.DefaultRequestHeaders.Clear();

                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                }

                HttpResponseMessage apiResponse = null;
                message.Method = apiRequest.ApiType;

                apiResponse = await client.SendAsync(message);


                var responseDto = new ResponseDto<T>
                {
                    IsSuccess = apiResponse.IsSuccessStatusCode,
                    ErrorMessages = apiResponse.RequestMessage.ToString(),
                    StatusCode = apiResponse.StatusCode.ToString()
                };



                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(apiContent);

                    responseDto.Result = result;

                }
                else
                {
                    _logger.LogError(string.Format("Error {0} - Status Code{1}", responseDto.ErrorMessages, responseDto.StatusCode));

                }


                return responseDto;


            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error in {0}: stack trace{1}", ex.Message, ex.StackTrace));
                return new ResponseDto<T> { ErrorMessages = ex.Message, IsSuccess = false };
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
