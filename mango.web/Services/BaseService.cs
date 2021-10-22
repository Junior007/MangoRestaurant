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

        public BaseService(IHttpClientFactory httpClient)
        {
            //this.responseModel = new ResponseDto();
            this.httpClient = httpClient;
        }


        public async Task<ResponseDto<T>> SendAsync<T>(ApiRequest apiRequest)
        {
            
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
                    

                };

                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(apiContent);

                    responseDto.DisplayMessage = "OK";
                    responseDto.Result = result;

                }
                else {
                    responseDto.DisplayMessage = "Error";
                    responseDto.ErrorMessages = apiResponse.RequestMessage.ToString();


                }

                return responseDto;


            }
            /*catch (Exception e)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }*/
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
