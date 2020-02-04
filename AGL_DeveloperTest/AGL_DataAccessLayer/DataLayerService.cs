using AGL_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AGL_DataAccessLayer
{
    public abstract class DataLayerService
    {
        /// <summary>
        /// A simple Http Request that returns a string or error
        /// </summary>
        public async Task<Response<string>> GetResponse(Uri Externaluri, Dictionary<string, string> Headers = null)
        {
            var response = new Response<string>();

            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage();
                    request.RequestUri = Externaluri;
                    if (Headers != null && Headers.Any())
                    {
                        Headers.ToList().ForEach(h =>
                        {
                            request.Headers.Add(h.Key, h.Value);
                        });
                    }
                    var httpResponse = await client.SendAsync(request);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        response.Data = responseContent;
                        return response;
                    }
                }
            }
            catch (Exception)
            {
                response.Errors.Add(ErrorMessages.ErrorC001_CannotConnectToServer);
            }
            return response;
        }

    }
}
