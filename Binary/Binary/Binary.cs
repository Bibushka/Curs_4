using System;
using Xunit;

namespace Binary
{
    public class Binary
    {
        [Fact]
        public void GetBinaryForm()
        {
            Assert.Equal(new byte[] { 1 }, ReturnBytes(1));
        }

        [Theory]
        public void GetNot(int first, int second)
        {
            Assert.Equal(ReturnBytes(first), NOT(second));
        }

        public byte[] ReturnBytes(int number)
        {
            byte[] yourByte= { };
            if (number == 0)
            {
                Array.Resize(ref yourByte, yourByte.Length + 1);
            }
            else
            {
                while (number > 0)
                {
                    Array.Resize(ref yourByte, yourByte.Length + 1);
                    yourByte[yourByte.Length - 1] = (byte)(number % 2);
                    number = number / 2;
                }
            }
            Array.Reverse(yourByte);
            return yourByte;
        }

        public byte[] NOT(int firstNumber)
        {
            byte[] yourByte = ReturnBytes(firstNumber);
            for (int i = 0; i < yourByte.Length; i++)
            {
                if (yourByte[i] == 0)
                    yourByte[i] = 1;
                yourByte[i] = 0;
            }
            return yourByte;
        }
    }
}
