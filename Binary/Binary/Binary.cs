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

        [Fact]
        public void GetBinaryFormForZero()
        {
            Assert.Equal(new byte[] { 0 }, ReturnBytes(0));
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(0, 7)]
        public void GetNot(int first, int second)
        {
            Assert.Equal(ReturnBytes(first), NOT(second));
        }

        [Theory]
        [InlineData(7, 5, 3)]
        [InlineData(10, 10, 10)]
        public void GetOR(int expected, int first, int second)
        {
            Assert.Equal(ReturnBytes(expected), OR(first, second));
        }

        [Fact]
        public void NoZeros()
        {
            Assert.Equal(new byte[] { 1, 0 }, EraseZeros(new byte[] { 0, 0, 1, 0 }));
        }

        public byte[] ReturnBytes(int number)
        {
            byte[] yourByte= { };
            if (number == 0)
            {
                Array.Resize(ref yourByte, yourByte.Length + 1);
                yourByte[0] = 0;
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
            int counter = 0;
            for (int i = 0; i < yourByte.Length; i++)
                if (yourByte[i] == 1)
                    counter++;
            if (counter == yourByte.Length)
            {
                byte[] result = { 0 };
                return result;
            }
            for (int i = 0; i < yourByte.Length; i++)
            {
                if (yourByte[i] == 0)
                    yourByte[i] = 1;
                else
                    yourByte[i] = 0;
            }
            return EraseZeros(yourByte);
        }

        public byte[] OR(int first, int second)
        {
            byte[] firstByte = ReturnBytes(first);
            byte[] secondByte = ReturnBytes(second);
            if (firstByte.Length > secondByte.Length)
            {
                Array.Reverse(secondByte);
                Array.Resize(ref secondByte, firstByte.Length);
                Array.Reverse(secondByte);
            }
            if (secondByte.Length > firstByte.Length)
            {
                Array.Reverse(firstByte);
                Array.Resize(ref firstByte, secondByte.Length);
                Array.Reverse(firstByte);
            }
            byte[] result = new byte[firstByte.Length];
            for (int i = 0; i < firstByte.Length; i++)
                if (firstByte[i] == 0 && secondByte[i] == 0)
                    result[i] = 0;
                else
                    result[i] = 1;
            return result;
        }

        public byte[] EraseZeros(byte[] yourByte)
        {
            int counter = 0;
            for (int i = 0; i < yourByte.Length; i++)
            {
                if(yourByte[i] == 0)
                    counter++;
                else
                    break;
            }
            Array.Reverse(yourByte);
            Array.Resize(ref yourByte, yourByte.Length - counter);
            Array.Reverse(yourByte);
            return yourByte;       
        }
    }
}
