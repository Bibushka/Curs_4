using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Binary
{
    [TestClass]
    public class Binary
    {
        [TestMethod]
        {
            CollectionAssert.AreEqual(new byte[] {0,0,0,1}, ReturnBytes(1));
        }

        public Array ReturnBytes(int number)
        {
            return new byte[] {0,0,0,0};
        }
    }
}
