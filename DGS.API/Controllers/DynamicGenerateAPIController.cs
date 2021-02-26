using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicGenerateAPIController : ControllerBase
    {
        //API for Generating a Series, as of now maximum 15 can be generated
        [HttpGet]
        public IActionResult GenerateNumbers(int mth = 15)
        {
            int[] series = GenerateSeries(mth);
            return Ok(series);
        }

        [HttpGet("divisor,number")]
        public IActionResult ResultFromSeries(int divisor, int number)
        {
            int flag = 0;
            int nth = 15;

            int[] series = GenerateSeries(nth);
            int result = -1;
            for (int i = 0; i < series.Length; i++)
            {
                if (series[i] % divisor == 0)
                    flag += 1;

                if (flag == number)
                {
                    result = series[i];
                    break;
                }
            }
            if (flag != number)
                return Ok(-1);

            return Ok(result);
        }



        private int[] GenerateSeries(int term)
        {
            int val1 = 1, val2 = 1, val3 = 1, val4;

            int[] ar = new int[term - 1];
            ar[0] = ar[1] = ar[2] = 1;

            for (int i = 3; i < term - 1; i++)
            {
                val4 = val1 + val2 + val3;
                ar[i] = val4;
                val1 = val2;
                val2 = val3;
                val3 = val4;
            }
            return ar;
        }



    }


}
