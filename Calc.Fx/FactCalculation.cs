using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Calc.Fx
{
    public sealed class FactCalculation
    {
        private const int TwoGig = 2147483591;
        private int _n;
        private Array _array;
        private string _result;
        private TimeSpan _time;

        public FactCalculation(int n)
        {
            if (n < 0)
                throw new Exception("Please enter n>=0 number");

            _n = n;

            var type = typeof(int);
            var size = Marshal.SizeOf(type);
            var num = TwoGig / size;

            _array = Array.CreateInstance(type, num);

        }

        public bool Calculate()
        {
            try
            {
                var counter = 0;
                var stopWatch = new Stopwatch();

                stopWatch.Start();
                _array.SetValue(1, 0);

                for (; _n >= 2; _n--)
                {
                    var temp = 0;

                    for (var i = 0; i <= counter; i++)
                    {
                        temp = ((int)_array.GetValue(i) * _n) + temp;
                        _array.SetValue(temp % 10, i);
                        temp = temp / 10;
                    }

                    while (temp > 0)
                    {
                        _array.SetValue(temp % 10, ++counter);
                        temp = temp / 10;
                    }
                }

                var sb = new StringBuilder();
                for (var i = counter; i >= 0; i--)
                    sb.Append(_array.GetValue(i));

                _result = sb.ToString();

                _time = stopWatch.Elapsed;
                stopWatch.Stop();

                return true;
            }
            catch (Exception)
            {
                throw;
            } 
        }

        public string Result
        {
            get { return _result; }
        }

        public string Elapsed
        {
            get { return _time.ToString("g"); }
        }

    }
}
