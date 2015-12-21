using System;
using System.Diagnostics;
using Calc.Fx;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calc.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [Description("test instance")]
        public void TestMethod1()
        {
            new FactCalculation(-2343432);
        }

        [TestMethod]
        [Description("calculate trace")]
        public void TestMethod2()
        {
            var calc = new FactCalculation(234);

            Assert.IsTrue(calc.Calculate(), "Can't calculate");

            Trace.TraceWarning(calc.Result);
            Trace.TraceWarning("{0} elapsed", calc.Elapsed);
        }

        [TestMethod]
        [Description("equal trace")]
        public void TestMethod3()
        {
            var calc = new FactCalculation(5);

            Assert.IsTrue(calc.Calculate(), "Can't calculate");
            Assert.AreEqual(calc.Result, "121", "Can't equls");

            Trace.TraceWarning(calc.Result);
            Trace.TraceWarning("{0} elapsed", calc.Elapsed);
        }
    }
}
