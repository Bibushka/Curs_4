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
            Assert.Equal(ReturnBytes(first), NOT(ReturnBytes(second)));
        }

        [Theory]
        [InlineData(7, 5, 3)]
        [InlineData(10, 10, 10)]
        public void GetOR(int expected, int first, int second)
        {
            Assert.Equal(ReturnBytes(expected), OR(ReturnBytes(first), ReturnBytes(second)));
        }

        [Theory]
        [InlineData(1, 5, 3)]
        [InlineData(5, 7, 5)]
        public void GetAND(int expected, int first, int second)
        {
            Assert.Equal(ReturnBytes(expected), AND(ReturnBytes(first), ReturnBytes(second)));
        }

        [Theory]
        [InlineData(6, 5, 3)] 
        [InlineData(8, 10, 2)] 
        public void GetXOR(int expected, int first, int second)
        {
            Assert.Equal(ReturnBytes(expected), XOR(ReturnBytes(first), ReturnBytes(second)));
        }

        [Theory]
        [InlineData(8, 1, 3)]
        [InlineData(20, 5, 2)]
        public void GetShiftLeft(int expected, int number, int step)
        {
            Assert.Equal(ReturnBytes(expected), ShiftLeft(ReturnBytes(number), step));
        }

        [Theory]
        [InlineData(1, 8, 3)]
        [InlineData(12, 50, 2)]
        public void GetShiftRight(int expected, int number, int step)
        {
            Assert.Equal(ReturnBytes(expected), ShiftRight(ReturnBytes(number), step));
        }

        [Theory]
        [InlineData(1, 8)]
        [InlineData(12, 50)]
        public void GetLessThen(int question, int answer)
        {
            Assert.True (LessThen(ReturnBytes(question), ReturnBytes(answer)));
        }

        [Theory]
        [InlineData(9, 8)]
        [InlineData(51, 50)]
        public void GetLessThenFail(int question, int answer)
        {
            Assert.False(LessThen(ReturnBytes(question), ReturnBytes(answer)));
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

        public byte[] NOT(byte[] yourByte)
        {
            for (int i = 0; i < yourByte.Length; i++)
            {
                if (yourByte[i] == 0)
                    yourByte[i] = 1;
                else
                    yourByte[i] = 0;
            }
            return EraseZeros(yourByte);
        }

        public byte[] OR(byte[] firstByte, byte[] secondByte)
        {
            byte[] result = new byte[firstByte.Length];
            for (int i = 0; i < firstByte.Length; i++)
                if (firstByte[i] == 0 && secondByte[i] == 0)
                    result[i] = 0;
                else
                    result[i] = 1;
            return EraseZeros(result);
        }

        public byte[] AND(byte[] firstByte, byte[] secondByte)
        {
            if (firstByte.Length > secondByte.Length)
                secondByte = ResizeByte(secondByte, firstByte.Length);
            if (secondByte.Length > firstByte.Length)
                firstByte = ResizeByte(firstByte, secondByte.Length);
            byte[] result = new byte[firstByte.Length];
            for (int i = 0; i < firstByte.Length; i++)
                if (firstByte[i] == 1 && secondByte[i] == 1)
                    result[i] = 1;
                else
                    result[i] = 0;

            return EraseZeros(result);
        }

        public byte[] XOR(byte[] firstByte, byte[] secondByte)
        {
            if (firstByte.Length > secondByte.Length)
                secondByte = ResizeByte (secondByte, firstByte.Length);
            if (secondByte.Length > firstByte.Length)
                firstByte = ResizeByte(firstByte, secondByte.Length);
            byte[] result = new byte[firstByte.Length];
            for (int i = 0; i < firstByte.Length; i++)
                if ((firstByte[i] == 1 || secondByte[i] == 1) && (firstByte[i] == 0 || secondByte[i] == 0))
                    result[i] = 1;
                else
                    result[i] = 0;
            return EraseZeros(result);
        }

        public byte[] ShiftLeft(byte[] yourByte, int step)
        {
            Array.Resize(ref yourByte, yourByte.Length + step);
            return EraseZeros(yourByte);
        }

        public byte[] ShiftRight(byte[] yourByte, int step)
        {
            Array.Resize(ref yourByte, yourByte.Length - step);
            return EraseZeros(yourByte);
        }

        public bool LessThen(byte[] firstByte, byte[] secondByte)
        {
            if (firstByte.Length < secondByte.Length)
                return true;
            if (firstByte.Length > secondByte.Length)
                return false;
            for (int i = 0; i < firstByte.Length; i++)
                if (firstByte[i] < secondByte[i])
                    return true;
            return false;
        }

        public byte[] ResizeByte(byte[] yourByte, int lenght)
        {
            Array.Reverse(yourByte);
            Array.Resize(ref yourByte, lenght);
            Array.Reverse(yourByte);
            return yourByte;
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
            Array.Resize(ref yourByte, (yourByte.Length == counter ? 1 : yourByte.Length - counter));
            Array.Reverse(yourByte);
            return yourByte;       
        }
    }
}
