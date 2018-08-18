using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RealNumberApp.Tests
{
    [TestClass]
    public class RealNumberTest
    {
        [DataTestMethod]
        [DataRow("0")]
        [DataRow("1")]
        [DataRow("0.5")]
        [DataRow(".5")]
        [DataRow("123")]
        [DataRow("-2")]
        [DataRow("+5")]
        [DataRow("-.5")]
        [DataRow("-.5")]
        [DataRow("-0.06")]
        [DataRow("000001")]
        public void PassedData_ShouldResolve_ToRealNumber(string number)
        {
            Assert.IsTrue(Number.IsRealNumber(number));
        }

        [DataTestMethod]
        [DataRow("1-1")]
        [DataRow("abc")]
        [DataRow("2..3")]
        [DataRow("2.")]
        [DataRow("--2")]
        [DataRow("-+6")]
        [DataRow("12a4")]
        [DataRow("47a-8")]
        [DataRow("1-.5")]
        [DataRow("-+0.06")]
        [DataRow("00000-")]
        [DataRow("+.-")]
        public void PassedData_ShouldNotResolve_ToRealNumber(string number)
        {
            Assert.IsFalse(Number.IsRealNumber(number));
        }
    }
}
