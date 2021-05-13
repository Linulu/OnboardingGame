using Newtonsoft.Json;
using OnboardingGame.Models;
using OnboardingGame.REST_Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingGame.Data
{
    public class RestConnection
    {
        HttpClient client;

        private string baseUri;

        public RestConnection(string uri) {
            client = new HttpClient();
            baseUri = uri;
        }

        public async Task<bool> ValidateUserAsync(string name, string password) {
            Uri uri = new Uri(baseUri + "/api/Application/validateuser");

            RestUser content = new RestUser {
                userName = name,
                passWord = password
            };

            string user = JsonConvert.SerializeObject(content);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri,
                Content = new StringContent(user, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode) {
                return true;
            }

            return false;
        }

        public async Task<List<JSON_Data>> GetDataAsync(string name, string password) {

            Uri uri = new Uri(baseUri + "/api/application/fetchlists");

            RestUser content = new RestUser {
                userName = name,
                passWord = password
            };

            string user = JsonConvert.SerializeObject(content);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri,
                Content = new StringContent(user, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode) {
                string resContent = await response.Content.ReadAsStringAsync();

                List<JSON_Data> data = JsonConvert.DeserializeObject<List<JSON_Data>>(resContent);

                return data;
            }

            return null;
        }

        public async Task<bool> UpdateStatusAsync(string name, string password, REST_Data.Task item) {

            if (item is null) {
                return false;
            }

            Uri uri = new Uri(baseUri + "/api/application");

            UpdatedTask JSONitem = new UpdatedTask
            {
                userName = name,
                passWord = password,
                taskName = item.title,
                status = item.status
            };

            string json = JsonConvert.SerializeObject(JSONitem);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
