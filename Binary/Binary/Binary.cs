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
        }

        public byte[] ReturnBytes(int number)
        {
            byte[] yourByte= { };
            {
                yourByte[yourByte.Length-1] = Convert.ToByte(number % 2);
                number = number / 2;
            }
            return yourByte;
        }
    }
}
