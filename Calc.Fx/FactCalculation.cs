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

        private static BigInteger ProdTree(int l, int r)
        {
            if (l > r)
                return 1;
            if (l == r)
                return l;
            if (r - l == 1)
                return (BigInteger)l * r;

            var m = (l + r) / 2;

            return ProdTree(l, m) * ProdTree(m + 1, r);
        }

        private static Task<BigInteger> ProdTreeAsync(int l, int r)
        {
            return Task<BigInteger>.Run(() => ProdTree(l, r));
        }

        private BigInteger CalculateMultithread()
        {
            var threadCount = Environment.ProcessorCount;

            if (_n < 0)
                return 0;

            switch (_n)
            {
                case 0:
                    return 1;
                case 1:
                case 2:
                    return _n;
            }

            if (_n < threadCount + 1)
                return ProdTree(2, _n);

            var tasks = new Task<BigInteger>[threadCount];

            tasks[0] = ProdTreeAsync(2, _n / threadCount);
            for (int i = 1; i < threadCount; i++)
            {
                tasks[i] = ProdTreeAsync(((_n / threadCount) * i) + 1, (_n / threadCount) * (i + 1));
            }

            Task<BigInteger>.WaitAll(tasks);

            BigInteger result = 1;

            for (var i = 0; i < threadCount; i++)
            {
                result *= tasks[i].Result;
            }

            return result;
        }

        public bool Calculate2()
        {
            try
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                var f = CalculateMultithread();

                _result = f.ToString();

                stopWatch.Stop();
                _time = stopWatch.Elapsed;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
