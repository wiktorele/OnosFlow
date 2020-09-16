using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnosFlow.Models;

namespace OnosFlow.Controllers
{
    public class FlowController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public IEnumerable<FlowModel> flowModel { get; set; }
        private readonly ILogger<FlowController> _logger;

        public FlowController(ILogger<FlowController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }


        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var url = "http://192.168.56.120:8181/onos/v1/";
            Uri baseUri = new Uri(url);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();

            string clientId = "onos";
            string clientPassword = "rocks";

            var authenticationString = $"{clientId}:{clientPassword}";

            //convert credentials to base64
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var request = new HttpRequestMessage(HttpMethod.Get, baseUri + "flows");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

            //result of request operation as .net operation
            var response = await client.SendAsync(request);

            //http response
            //var response = task.Result;

            response.EnsureSuccessStatusCode();
            //string responseBody = response.Content.ReadAsStringAsync().Result;

            var responseBody = response.Content.ReadAsStreamAsync().Result;

            flowModel = await JsonSerializer.DeserializeAsync<IEnumerable<FlowModel>>(responseBody);

            //ViewData["Response"] = responseBody;
            ViewData["Flow"] = flowModel;

            return View(flowModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
