using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Binary
{
    [TestClass]
    public class Binary
    {
        [TestMethod]
        public void GetBinaryForm()
        {
            Assert.AreEqual(new byte[] {1}, ReturnBytes(1));
        }

        public byte[] ReturnBytes(int number)
        {
            byte[] yourByte= { };
            while (number!=0)
            {
                Array.Resize(ref yourByte, 1);
                yourByte[yourByte.Length-1] = Convert.ToByte(number % 2);
                number = number / 2;
            }
            return yourByte;
        }
    }
}
