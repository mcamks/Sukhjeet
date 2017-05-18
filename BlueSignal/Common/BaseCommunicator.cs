using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace BlueSignal.Common
{
    public static class BaseCommunicator<T>
    {
        //public static async Task<T> PostAsync<T>(string url, object obj)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var response = await client.PostAsJsonAsync(url, obj);
        //        //return response.Res;
        //    }
        //}


        //private static async Task<TResponse> CreateJsonResponse<TResponse>(HttpResponseMessage response) where TResponse : ApiResponse, new()
        //{
        //    var clientResponse = new TResponse
        //    {
        //        StatusIsSuccessful = response.IsSuccessStatusCode,
        //        ResponseCode = response.StatusCode
        //    };
        //    if (response.Content != null)
        //    {
        //        clientResponse.ResponseResult = await response.Content.ReadAsStringAsync();
        //    }

        //    return clientResponse;
        //}
    }
}