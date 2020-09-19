using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using OnosFlow.Data;
using OnosFlow.Models;
using static OnosFlow.Models.FlowModel;

namespace OnosFlow.Controllers
{
    public class FlowController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly Context _context;
        public IEnumerable<FlowModel> flowModel { get; set; }
        private readonly ILogger<FlowController> _logger;

        public FlowController(ILogger<FlowController> logger, IHttpClientFactory clientFactory, Context context)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _context = context;
        }

        /// <summary>
        /// Display list of flows.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("onosBase");

            var dbConfig = _context.Configs.First();
            string clientId = dbConfig.UserName;
            string clientPassword = dbConfig.Password;

            var authenticationString = $"{clientId}:{clientPassword}";
            //convert credentials to base64
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var request = new HttpRequestMessage(HttpMethod.Get, "v1/flows");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            request.Headers.Add("Accept", "application/json");

            //result of request operation as .net operation
            var response = await client.SendAsync(request);

            //check authorization
            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("UnauthorizedRequest");
            }

            //http response
            //var response = task.Result;
            string errorString;

            if (response.IsSuccessStatusCode)
            {
                //string responseBody = response.Content.ReadAsStringAsync().Result;
                var responseBody = await response.Content.ReadFromJsonAsync<FlowModel>();
                return View(responseBody);
            }
            else
            {
                errorString = $"There was an error getting flows: { response.ReasonPhrase }";
                return Content(errorString);
            }

            //flowModel = await JsonSerializer.DeserializeAsync<IEnumerable<FlowModel>>(responseBody);

            //ViewData["Response"] = responseBody;
            //ViewData["Flow"] = flowModel;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UnauthorizedRequest()
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
