using MyPhysio.Domain.ServiceModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyPhysioAPI.v1.Extensions.Datatypes
{

    /// <summary>
    /// 
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="clientid"></param>
        /// <param name="secret"></param>
        /// <param name="endPoint"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        public  static string GetAccessToken(this string baseAddress, string clientid,string secret, string endPoint)
        {
            try
            {

                string base64Encoded = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientid}:{secret}"));

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Encoded);

                    var request = new HttpRequestMessage(HttpMethod.Post, endPoint);                   

                    var httpResponseMessage = client.SendAsync(request).GetAwaiter().GetResult();

                    using var contentStream =
                            httpResponseMessage.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
                    
                    var accessTokenObject =  JsonSerializer.DeserializeAsync
                                   <AccessToken>(contentStream).GetAwaiter().GetResult();

                    var isvalid = accessTokenObject.token.access_token.ValidateAccessToken();

                    return accessTokenObject.token.access_token;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static bool ValidateAccessToken( this string accessToken)
        {
            try
            {
                var validatedToken = new JwtSecurityTokenHandler()
                 .ReadToken(accessToken) as JwtSecurityToken;
                              
                if (validatedToken.ValidTo > System.DateTime.Now) return true;
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static string GetTokenClient(this string accessToken)
        {
            try
            {
                var validatedToken = new JwtSecurityTokenHandler()
                 .ReadToken(accessToken) as JwtSecurityToken;
                var accountName = validatedToken.Payload.FirstOrDefault(x => x.Key == "AccountName").Value;
                return accountName.ToString();
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
