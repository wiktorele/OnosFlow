﻿
using Microsoft.EntityFrameworkCore.Internal;
using OnosFlow.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
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

            client.DefaultRequestHeaders.Clear();

            //get config from InMemoryDataBase
            var dbConfig = _context.Configs.First();
            string clientId = dbConfig.UserName;
            string clientPassword = dbConfig.Password;
            string IpAddress = dbConfig.IpAddress;

            url = "http://" + IpAddress +":8181/onos/";

            client.BaseAddress = new Uri(url);
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

        //parse response from json and display in view
        public async Task<FlowModel> GetFlow(string deviceId, string flowId)
        {
            var response = await Client.GetAsync($"v1/flows/{deviceId}/{flowId}");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadFromJsonAsync<FlowModel>();

            return responseBody;
        }

        public async Task<HttpResponseMessage> PostFlow(string deviceId, Flow postFlow)
        { 

            var response = await Client.PostAsJsonAsync($"v1/flows/{deviceId}", postFlow);

            response.EnsureSuccessStatusCode();

            //var postResponse = await response.Content.ReadFromJsonAsync<Flow>();

            return response;
        }

        public async Task<HttpResponseMessage> DeleteFlow(string deviceId, string flowId)
        {
            var response = await Client.DeleteAsync($"v1/flows/{deviceId}/{flowId}");

            response.EnsureSuccessStatusCode();

            return response;
        }

    }
}
