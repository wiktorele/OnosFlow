using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using OnosFlow.Data;
using OnosFlow.Models;
using static OnosFlow.Models.FlowModel;

namespace OnosFlow.Controllers
{
    public class FlowController : Controller
    {
        //private readonly IHttpClientFactory _clientFactory;
        private readonly OnosService _onosService;
        public IEnumerable<FlowModel> flowModel { get; set; }
        private readonly ILogger<FlowController> _logger;
        public FlowController(ILogger<FlowController> logger, OnosService onosService)
        {
            _logger = logger;
            _onosService = onosService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var getFlows = await _onosService.GetFlowsList();
                return View(getFlows);
            }
            catch (HttpRequestException e)
            {
                if (e.Message == "Response status code does not indicate success: 401 (Unauthorized).")
                {
                    return RedirectToAction("UnauthorizedRequest");
                }
                else
                {
                    string error = e.Message;
                    string errorString = $"There was an error getting flows: { error }";
                    return Content(errorString);
                }
            }
        }

        public async Task<IActionResult> Details(string deviceId, string flowId)
        {
            try
            {
                var getFlow = await _onosService.GetFlow(deviceId, flowId);
                var flow = getFlow.flows[0];
                return View(flow);
            }
            catch (HttpRequestException e)
            {
                if (e.Message == "Response status code does not indicate success: 401 (Unauthorized).")
                {
                    return RedirectToAction("UnauthorizedRequest");
                }
                else
                {
                    string error = e.Message;
                    string errorString = $"There was an error getting flows: { error }";
                    return Content(errorString);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string deviceId, Flow flow)
        {
            if (!ModelState.IsValid)
            {
                return View(flow);
            }
            try
            {
                var createFlow = await _onosService.PostFlow(deviceId, flow);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException e)
            {
                if (e.Message == "Response status code does not indicate success: 401 (Unauthorized).")
                {
                    return RedirectToAction("UnauthorizedRequest");
                }
                else
                {
                    string error = e.Message;
                    string errorString = $"There was an error getting flows: { error }";
                    return Content(errorString);
                }
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditConfirmed(string deviceId, Flow flow)
        {
            if (!ModelState.IsValid)
            {
                return View(flow);
            }
            try
            {
                var createFlow = await _onosService.PostFlow(deviceId, flow);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException e)
            {
                if (e.Message == "Response status code does not indicate success: 401 (Unauthorized).")
                {
                    return RedirectToAction("UnauthorizedRequest");
                }
                else
                {
                    string error = e.Message;
                    string errorString = $"There was an error getting flows: { error }";
                    return Content(errorString);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string deviceId, string flowId)
        {
            ViewData["flowId"] = flowId;
            ViewData["deviceId"] = deviceId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string deviceId, string flowId)
        {
            try
            {
                var deleteFlow = await _onosService.DeleteFlow(deviceId, flowId);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException e)
            {
                if (e.Message == "Response status code does not indicate success: 401 (Unauthorized).")
                {
                    return RedirectToAction("UnauthorizedRequest");
                }
                else
                {
                    string error = e.Message;
                    string errorString = $"There was an error deleting flow: { error }";
                    return Content(errorString);
                }
            }
        }

        [HttpGet]
        public IActionResult Delete(string deviceId, string flowId)
        {
            ViewData["flowId"] = flowId;
            ViewData["deviceId"] = deviceId;

            return View();
        }
        public IActionResult UnauthorizedRequest()
        {
            return View();
        }
    }
}
