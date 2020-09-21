
using OnosFlow.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OnosFlow.Models
{
    public class OnosService
    {
        string url = "http://192.168.56.120:8181/onos/";
        private readonly Context _context;
        public HttpClient Client { get; }

        public OnosService(HttpClient client, Context context)
        {
            _context = context;

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Clear();

            //get config from InMemoryDataBase
            var dbConfig = _context.Configs.First();
            string clientId = dbConfig.UserName;
            string clientPassword = dbConfig.Password;
             //set config as authentication credentials
            var authenticationString = $"{clientId}:{clientPassword}";
            //convert credentials to base64
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
            //add authorization with your credentials
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            Client = client;
        }

        public async Task<FlowModel> GetFlowsList()
        {
            var response = await Client.GetAsync("v1/flows");

            response.EnsureSuccessStatusCode();

            //parse response from json and display in view
            var responseBody = await response.Content.ReadFromJsonAsync<FlowModel>();

            return responseBody;
        }

        public async Task<HttpResponseMessage> DeleteFlow(string deviceId, string flowId)
        {
            var response = await Client.DeleteAsync($"v1/flows/{deviceId}/{flowId}");

            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task<HttpResponseMessage> GetFlow(string deviceId, string flowId)
        {
            var response = await Client.GetAsync($"v1/flows/{deviceId}/{flowId}");

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadFromJsonAsync<FlowModel>();//??????????????????????

            return response;
        }

        //public async Task<HttpResponseMessage> UpdateFlow(string deviceId, string flowId)
        //{
        //    var response = await Client.PutAsync($"v1/flows/{deviceId}/{flowId}"); ??????

        //    response.EnsureSuccessStatusCode();

        //    return response;
        //}

        //public async Task<HttpResponseMessage> CreateFlow(string deviceId)
        //{
        //    var response = await Client.DeleteAsync($"v1/flows/{deviceId}/{flowId}");

        //    response.EnsureSuccessStatusCode();

        //    return response;
        //}

    }
}
