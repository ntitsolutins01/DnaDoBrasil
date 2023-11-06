using System.Text;
using Newtonsoft.Json;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {

        private readonly HttpClient _httpClient;
        private Uri BaseEndpoint { get; }
        
        public DnaApiClient(Uri baseEndpoint)
        {
            BaseEndpoint = baseEndpoint ?? throw new ArgumentNullException(nameof(baseEndpoint));
            _httpClient = new HttpClient();
        }

        private T Get<T>(Uri requestUrl)
        {
            addHeaders();
            var response = _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            var data = response.Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data.Result);
        }

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        private Task<long> Post<T>(Uri requestUrl, T content)
        {
            addHeaders();
             var response = _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            var data = response.Result.Content.ReadAsStringAsync();
            return Task.FromResult(JsonConvert.DeserializeObject<long>(data.Result));
        }

        /// <summary>
        /// Common method for making PUT calls
        /// </summary>
        private Task<long> Put<T>(Uri requestUrl, T content)
        {
            addHeaders();
             var response = _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            var data = response.Result.Content.ReadAsStringAsync();
            return Task.FromResult(JsonConvert.DeserializeObject<long>(data.Result));
        }
        //private async Task<MessageModel<T>> PostAsync<T>(Uri requestUrl, T content)
        //{
        //    addHeaders();
        //    var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
        //    response.EnsureSuccessStatusCode();
        //    var data = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<MessageModel<T>>(data);
        //}
        private async Task<T1> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            addHeaders();
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T1>(data);
        }

        private Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

        private void addHeaders()
        {
            _httpClient.DefaultRequestHeaders.Remove("userIP");
            _httpClient.DefaultRequestHeaders.Add("userIP", "192.168.1.1");

            //_httpClient.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("Bearer", "Your Oauth token");
        }
    }
}
