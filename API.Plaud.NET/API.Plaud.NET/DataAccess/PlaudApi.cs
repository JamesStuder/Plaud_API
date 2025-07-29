using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using API.Plaud.NET.Constants;
using API.Plaud.NET.Models.Responses;
using Newtonsoft.Json;

namespace API.Plaud.NET.DataAccess
{
    /// <summary>
    /// Represents the API client for Plaud, designed to handle communication with the Plaud API.
    /// Provides methods for authenticating, retrieving, and posting data to the API using HTTP requests.
    /// </summary>
    internal class PlaudApi
    {
        /// <summary>
        /// Represents an instance of <see cref="HttpClient"/> used for handling HTTP requests
        /// and communication with the Plaud API, including setting the base address
        /// and configuring headers for authentication or specific API operations.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Represents the API client for Plaud, designed to handle communication with the Plaud API.
        /// Provides methods for authenticating, retrieving, and posting data to the API using HTTP requests.
        /// </summary>
        internal PlaudApi()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Endpoints.BaseUrl);
        }

        /// <summary>
        /// Sends a POST request to authenticate with the API using provided credentials and retrieves an authentication response.
        /// </summary>
        /// <param name="username">The username to be used for authentication.</param>
        /// <param name="password">The password associated with the provided username.</param>
        /// <returns>A Task representing the asynchronous operation, with a result containing the authentication response in the form of <see cref="ResponseAuth"/>.</returns>
        /// <exception cref="Exception">Thrown if the authentication request fails or the response cannot be processed.</exception>
        internal async Task<ResponseAuth> AuthWithApiAsync(string username, string password)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();

                var formData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("client_id", "web")
                };

                using (FormUrlEncodedContent content = new FormUrlEncodedContent(formData))
                {
                    using (HttpResponseMessage response = await _httpClient.PostAsync(Endpoints.Authentication, content))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseAuth>(responseContent);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("AuthWithApi: " + ex.Message, ex);
            }
        }


        /// <summary>
        /// Sends a GET request to a specified endpoint using the provided authentication token, and deserializes the response into the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object expected in the API response.</typeparam>
        /// <param name="endpoint">The API endpoint to which the GET request is sent.</param>
        /// <param name="accessToken">The authorization token to be included in the request header for authentication.</param>
        /// <returns>A Task representing the asynchronous operation, with a result of the specified type <typeparamref name="T"/>.</returns>
        /// <exception cref="Exception">Thrown if the GET request fails or the response cannot be deserialized.</exception>
        internal async Task<T> GetDataAsync<T>(string endpoint, string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                throw new Exception($"GetDataAsync: {ex.Message}",ex);
            }
        }

        /// <summary>
        /// Sends a POST request to a specified endpoint with the provided data and authentication token.
        /// </summary>
        /// <typeparam name="T">The type of the response object expected from the API.</typeparam>
        /// <param name="endpoint">The API endpoint to which the POST request is sent.</param>
        /// <param name="dataToPost">The data object to be serialized and included in the POST request body.</param>
        /// <param name="accessToken">The authorization token to be included in the request header for authentication.</param>
        /// <returns>A Task representing the asynchronous operation, with a result of the specified type <typeparamref name="T"/>.</returns>
        /// <exception cref="Exception">Thrown if the POST request fails or the response cannot be deserialized.</exception>
        internal async Task<T> PostDataAsync<T>(string endpoint, object dataToPost, string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                string jsonData = JsonConvert.SerializeObject(dataToPost);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (Exception ex)
            {
                throw new Exception($"PostDataAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Sends a POST request to the specified endpoint with the provided data and access token
        /// but does not process any response body. Returns a boolean indicating success or failure.
        /// </summary>
        /// <param name="endpoint">The API endpoint where the POST request will be sent.</param>
        /// <param name="dataToPost">The data object to be serialized and sent in the request body.</param>
        /// <param name="accessToken">The access token used to authorize the request.</param>
        /// <returns>A boolean indicating whether the POST request was successful.</returns>
        /// <exception cref="Exception">Throws an exception if the request fails or an error occurs.</exception>
        internal async Task<bool> PostDataAsyncNoResponseBody(string endpoint, object dataToPost, string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                string jsonData = JsonConvert.SerializeObject(dataToPost);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"PostDataAsyncNoResponseBody: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Sends an HTTP DELETE request to the specified endpoint with a JSON-formatted request body
        /// and does not expect a response body from the server.
        /// </summary>
        /// <param name="endpoint">The URI of the API endpoint where the DELETE request will be sent.</param>
        /// <param name="dataUsedForDelete">The data to be serialized and sent in the request body.</param>
        /// <param name="accessToken">The Bearer token for authorizing the request.</param>
        /// <returns>Returns a boolean indicating whether the request was successful.</returns>
        /// <exception cref="Exception">Thrown when the HTTP request fails or an error occurs during execution.</exception>
        internal async Task<bool> DeleteRequestWithBodyAsyncNoResponseBody(string endpoint, object dataUsedForDelete, string accessToken)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(dataUsedForDelete);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                HttpRequestMessage deleteRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(endpoint, UriKind.Relative),
                    Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
                };
                
                HttpResponseMessage response = await _httpClient.SendAsync(deleteRequest);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"DeleteRequestWithBodyAsyncNoResponseBody: {ex.Message}", ex);
            }
        }
    }
}