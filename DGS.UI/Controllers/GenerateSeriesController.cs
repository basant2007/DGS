using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DGS.UI.Controllers
{
    public class GenerateSeriesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Uri _baseUri = null;
        private readonly HttpClient _httpClient = null;
        public GenerateSeriesController(IConfiguration Configuration)
        {
            _configuration = Configuration;
            _baseUri = new Uri(_configuration["ApiUrl"]);
            _httpClient = new HttpClient
            {
                BaseAddress = _baseUri
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FetchDataSeries(int mnumber)
        {
            var response = _httpClient.GetAsync(_httpClient.BaseAddress + $"APigenerate?mth={mnumber}").Result;
            if (response.IsSuccessStatusCode)
            {
                var getData = response.Content.ReadAsStringAsync().Result;
                ViewBag.Data = getData;
                TempData["series"] = getData;
            }
            else
            {
                ViewBag.Data = response.StatusCode;
                TempData["series"] = null;
            }
            return View();
        }

        [HttpPost]
        public IActionResult ShowDataSeries(int divisor, int number)
        {

            int flag = 0;
            //int[] series = TempData?.Peek("series") as int[];
            var series = TempData?.Peek("series").ToString().Replace("[", "").Replace("]", "").Split(',');
            for (int i = 0; i < series.Length; i++)
            {
                if (int.Parse(series[i]) % divisor == 0)
                    flag += 1;

                if (flag == number)
                {
                    ViewBag.Result = $"The number from the series is {series[i]}";
                    break;
                }

            }
            if (flag != number)
                ViewBag.Result = "No divisor found";
            return View("GetResult");
        }


    }
}
