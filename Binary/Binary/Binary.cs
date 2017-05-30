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
            CollectionAssert.AreEqual(new byte[] {0,0,0,1}, ReturnBytes(1, 2));
        }

        public Array ReturnBytes(int number, int prefferedBase)
        {
            int[] yourByte = new int[4];
            for(int i=yourByte.Length; i>=0; i--)
            {
                yourByte[i] = number % prefferedBase;
                number = number / prefferedBase;
            }
            return yourByte;
        }
    }
}
