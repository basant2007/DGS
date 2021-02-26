using DGS.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace DGS.Test
{
    public class UnitTest1
    {
        private const int limitSeries = 15;
        [Fact]
        public void Test1()
        {
            //AAA
            int divisor = 10;
            int number = 20;
            
            var expected = ResultFromSeries(divisor, number);

            var objCtl = new DynamicGenerateAPIController();

            var actual = objCtl.ResultFromSeries(divisor, number) as OkObjectResult;

            Assert.Equal(expected.ToString(), actual.Value.ToString());
        }

        private int[] GetDataForTest(int term)
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

        public int ResultFromSeries(int divisor, int number)
        {
            int flag = 0;

            int[] series = GetDataForTest(limitSeries);
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
                return -1;

            return result;
        }
    }
}
