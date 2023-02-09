using MyPhysio.Domain.Enums;
using MyPhysio.Infrastructure.Contracts;
using Microsoft.Net.Http.Headers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyPhysio.Infrastructure.Services
{

    /// <summary>
    /// This class is geneic for all the http client calls
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class ServiceClient<T, U> : IServiceClient<T, U> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;

        public ServiceClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<U> Get(string endpoint, HTTPClients clients)
        {
            try
            {
                using (var client = _clientFactory.CreateClient(clients.ToString()))
                {
                    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, endpoint);


                    httpRequestMessage.Content = new StringContent(string.Empty,
                                                        Encoding.UTF8,
                                                        "application/json");

                    var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                    
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        using var contentStream =
                            await httpResponseMessage.Content.ReadAsStreamAsync();

                        var response = await JsonSerializer.DeserializeAsync
                                       <U>(contentStream);

                        return response;
                    }
                    return default ;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        public async Task<IEnumerable<U>> GetCollection(string endpoint, HTTPClients clients)
        {
            try
            {
                using (var client = _clientFactory.CreateClient(clients.ToString()))
                {
                    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, endpoint);

                    var httpResponseMessage = await client.SendAsync(httpRequestMessage);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        using var contentStream =
                            await httpResponseMessage.Content.ReadAsStreamAsync();

                        var response = await JsonSerializer.DeserializeAsync
                                       <IEnumerable<U>>(contentStream);

                        return response;
                    }
                    return default;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<U> Post(T requestParameters, string endpoint, HTTPClients clients)
        {
            try
            {

                using (var client = _clientFactory.CreateClient(clients.ToString()))
                {
                    var postRequest = new StringContent(
                                      JsonSerializer.Serialize(requestParameters),
                                      Encoding.UTF8,
                                      Application.Json); 

                    using var httpResponseMessage =
                        await client.PostAsync(endpoint, postRequest);

                    httpResponseMessage.EnsureSuccessStatusCode();

                    var httpresponse = await httpResponseMessage.Content.ReadAsStreamAsync();

                    var result= await JsonSerializer.DeserializeAsync
                                       <U>(httpresponse);
                    return result;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<U> Patch(T requestParameters, string endpoint, HTTPClients clients)
        {
            try
            {

                using (var client = _clientFactory.CreateClient(clients.ToString()))
                {
                    var postRequest = new StringContent(
                                      JsonSerializer.Serialize(requestParameters),
                                      null,
                                   "application/merge-patch+json");

                    using var httpResponseMessage =
                        await client.PatchAsync(endpoint, postRequest);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {

                        var httpresponse = await httpResponseMessage.Content.ReadAsStreamAsync();

                        var result = await JsonSerializer.DeserializeAsync
                                           <U>(httpresponse);
                        return result;
                    }

                    return default;
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<U> Put(T requestParameters, string endpoint, HTTPClients clients)
        {
            try
            {

                using (var client = _clientFactory.CreateClient(clients.ToString()))
                {
                    var postRequest = new StringContent(
                                      JsonSerializer.Serialize(requestParameters),
                                      Encoding.UTF8,
                                      Application.Json);

                    using var httpResponseMessage =
                        await client.PutAsync(endpoint, postRequest);

                    httpResponseMessage.EnsureSuccessStatusCode();

                    var httpresponse = await httpResponseMessage.Content.ReadAsStreamAsync();

                    var result = await JsonSerializer.DeserializeAsync
                                       <U>(httpresponse);
                    return result;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<U>> PostCollection(T requestParameters, string endpoint, HTTPClients clients)
        {
            try
            {

                using (var client = _clientFactory.CreateClient(clients.ToString()))
                {
                    var postRequest = new StringContent(
                                      JsonSerializer.Serialize(requestParameters),
                                      Encoding.UTF8,
                                      Application.Json);

                    using var httpResponseMessage =
                        await client.PostAsync(endpoint, postRequest);

                    httpResponseMessage.EnsureSuccessStatusCode();

                    var httpresponse = await httpResponseMessage.Content.ReadAsStreamAsync();

                    var result = await JsonSerializer.DeserializeAsync
                                       <IEnumerable<U>>(httpresponse);
                }
                return default;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
