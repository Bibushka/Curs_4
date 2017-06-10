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
            Assert.AreEqual(new byte[] {1, 0}, ReturnBytes(2, 2));
        }
        

        public byte[] ReturnBytes(int number, int chosenBase)
        {
            byte[] yourByte= { };
            while (number!=0)
            {
                Array.Resize(ref yourByte, yourByte.Length+1);
                yourByte[yourByte.Length-1] = Convert.ToByte(number % chosenBase);
                number = number / chosenBase;
            }
            Array.Reverse(yourByte);
            return yourByte;
        }
    }
}
