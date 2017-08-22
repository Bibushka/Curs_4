using System;
using Xunit;

namespace Binary
{
    public class Binary
    {
        [Fact]
        public void GetBinaryForm()
        {
            Assert.Equal(new byte[] { 1, 2 }, ReturnBytes(5, 3));
        }

        [Fact]
        public void GetBinaryFormForZero()
        {
            Assert.Equal(new byte[] { 0 }, ReturnBytes(0, 2));
        }

        [Theory]
        [InlineData(1, 7, 2, 2)]
        [InlineData(0, 6, 3, 2)]
        public void IsGetAtGood(int returnedValue, int yourByte, int position, int ByteBase)
        {
            Assert.Equal(returnedValue, GetAt(ReturnBytes(yourByte, ByteBase), position));
        }

        [Theory]
        [InlineData(0, 1, 2)]
        public void GetNot(int first, int second, int ByteBase)
        {
            Assert.Equal(ReturnBytes(first, ByteBase), NOT(ReturnBytes(second, ByteBase)));
        }

        [Theory]
        [InlineData(7, 5, 3, 2)]
        public void GetOR(int expected, int first, int second, int byteBase)
        {
            Assert.Equal(ReturnBytes(expected, byteBase), OR(ReturnBytes(first, byteBase), ReturnBytes(second, byteBase)));
        }

        [Theory]
        [InlineData(3, 7, 3, 2)]
        public void GetAND(int expected, int first, int second, int byteBase)
        {
            Assert.Equal(ReturnBytes(expected, byteBase), AND(ReturnBytes(first, byteBase), ReturnBytes(second, byteBase)));
        }

        [Theory]
        [InlineData(6, 5, 3, 2)]
        public void GetXOR(int expected, int first, int second, int byteBase)
        {
            Assert.Equal(ReturnBytes(expected, byteBase), XOR(ReturnBytes(first, byteBase), ReturnBytes(second, byteBase)));
        }

        [Theory]
        [InlineData(8, 1, 3, 2)]
        public void GetShiftLeft(int expected, int number, int step, int byteBase)
        {
            Assert.Equal(ReturnBytes(expected, byteBase), ShiftLeft(ReturnBytes(number, byteBase), step));
        }

        [Theory]
        [InlineData(1, 8, 3, 2)]
        public void GetShiftRight(int expected, int number, int step, int byteBase)
        {
            Assert.Equal(ReturnBytes(expected, byteBase), ShiftRight(ReturnBytes(number, byteBase), step));
        }

        [Theory]
        [InlineData(4, 4, 2)]
        public void GetComparisonEqual(int first, int second, int byteBase)
        {
            Assert.Equal(true, Compare(ReturnBytes(first, byteBase), ReturnBytes(second, byteBase)));
        }
        [Theory]
        [InlineData(4, 3, 2)]
        public void GetComparisonGreater(int first, int second, int byteBase)
        {
            Assert.Equal(true, Compare(ReturnBytes(first, byteBase), ReturnBytes(second, byteBase)));
        }


        [Theory]
        [InlineData(4, 7, 2)]
        public void GetComparisonLess(int first, int second, int byteBase)
        {
            Assert.Equal(true, Compare(ReturnBytes(first, byteBase), ReturnBytes(second, byteBase)));
        }

        [Theory]
        [InlineData(5, 8, 2)]
        public void GetNotEqualTrue(int first, int second, int byteBase)
        {
            Assert.Equal(true, NotEqual(ReturnBytes(first, byteBase), ReturnBytes(second, byteBase)));
        }

        [Theory]
        [InlineData(5, 5, 2)]
        public void GetNotEqualFalse(int first, int second, int byteBase)
        {
            Assert.Equal(false, NotEqual(ReturnBytes(first, byteBase), ReturnBytes(second, byteBase)));
        }

        [Theory]
        [InlineData(4, 2, 2, 2)]
        [InlineData(8, 5, 3, 2)]
        public void GetSum(int result, int first, int second, int byteBase)
        {
            Assert.Equal(ReturnBytes(result, byteBase), Sum(ReturnBytes(first, byteBase), ReturnBytes(second, byteBase), byteBase));
        }

        [Theory]
        [InlineData(6, 2, 3, 2)]
        [InlineData(10, 5, 2, 2)]
        public void GetProduct(int result, int multiplicand, int multiplier, int byteBase)
        {
            Assert.Equal(ReturnBytes(result, byteBase), Product(ReturnBytes(multiplicand, byteBase), multiplier, byteBase));
        }

        [Theory]
        [InlineData(12, 15, 3, 2)]
        [InlineData(6, 9, 3, 3)]
        [InlineData(0, 5, 5, 4)]
        public void GetSubtraction(int result, int minuend, int subtrahend, int byteBase)
        {
            Assert.Equal(ReturnBytes(result, byteBase), Subtraction(ReturnBytes(minuend, byteBase), ReturnBytes(subtrahend, byteBase), byteBase));
        }

        [Theory]
        [InlineData(5, 15, 3, 3)]
        public void GetDivision(int result, int dividend, int divisor, int byteBase)
        {
            Assert.Equal(ReturnBytes(result, byteBase), Division(ReturnBytes(dividend, byteBase), ReturnBytes(divisor, byteBase), byteBase));
        }

        [Fact]
        public void NoZeros()
        {
            Assert.Equal(new byte[] { 1, 0 }, EraseZeros(new byte[] { 0, 0, 1, 0 }));
        }

        public byte[] ReturnBytes(int number, int byteBase)
        {
            byte[] yourByte = { };
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
                    yourByte[yourByte.Length - 1] = (byte)(number % byteBase);
                    number = number / byteBase;
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
            byte[] result = new byte[Math.Max(firstByte.Length, secondByte.Length)];
            for (int i = 0; i < result.Length; i++)
                if (GetAt(firstByte, i) == 1 && GetAt(secondByte, i) == 1)
                    result[i] = 1;
                else
                    result[i] = 0;
            Array.Reverse(result);
            return EraseZeros(result);
        }

        public byte[] XOR(byte[] firstByte, byte[] secondByte)
        {
            byte[] result = new byte[Math.Max(firstByte.Length, secondByte.Length)];
            for (int i = 0; i < result.Length; i++)
                if (GetAt(firstByte, i) != GetAt(secondByte, i))
                    result[i] = 1;
                else
                    result[i] = 0;
            Array.Reverse(result);
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

        public bool Compare(byte[] firstByte, byte[] secondByte)
        {
            if (firstByte.Length > secondByte.Length)
                return true;
            if (firstByte.Length < secondByte.Length)
                return true;
            for (int i = firstByte.Length - 1; i >= 0; i--)
            {
                if (GetAt(firstByte, i) > GetAt(secondByte, i))
                    return true;
                if (GetAt(firstByte, i) < GetAt(secondByte, i))
                    return true;
            }
            int c = 1;
            for (int i = firstByte.Length - 1; i >= 0; i--)
                if (GetAt(firstByte, i) != GetAt(secondByte, i))
                    c = 0;
            if (c == 1)
                return true;
            return false;
        }

        public bool NotEqual(byte[] firstByte, byte[] secondByte)
        {
            if (firstByte.Length != secondByte.Length)
                return true;
            for (int i = firstByte.Length - 1; i >= 0; i--)
                if (GetAt(firstByte, i) != GetAt(secondByte, i))
                    return true;
            return false;
        }

        public byte[] Sum(byte[] firstByte, byte[] secondByte, int byteBase)
        {
            byte[] result = new byte[Math.Max(firstByte.Length, secondByte.Length)];
            int counter = 0;
            for (int i = 0; i < result.Length; i++)
            {
                var sum = GetAt(firstByte, i) + GetAt(secondByte, i) + counter;
                result[i] = (byte)(sum % byteBase);
                counter = sum / byteBase;
            }
            if (counter == 1)
            {
                result = ResizeByte(result, result.Length + 1);
                result[0] = (byte)counter;
            }
            return result;
        }

        public byte[] Product(byte[] yourByte, int multiplier, int byteBase)
        {
            byte[] result = new byte[yourByte.Length];
            while (multiplier != 0)
            {
                result = Sum(result, yourByte, byteBase);
                multiplier--;
            }
            return EraseZeros(result);
        }

        public byte[] Subtraction(byte[] firstByte, byte[] secondBye, int byteBase)
        {
            byte[] result = new byte[Math.Max(firstByte.Length, secondBye.Length)];
            int counter = 0;
            for (int i = 0; i < result.Length; i++)
            {
                var dif = byteBase + GetAt(firstByte, i) - GetAt(secondBye, i) - counter;
                result[result.Length - i - 1] = (byte)(dif % byteBase);
                counter = dif < byteBase ? 1 : 0;
            }
            return EraseZeros(result);
        }

        public byte[] Division(byte[] yourByte, byte[] divisorByte, int byteBase)
        {
            byte[] compareByte = { 0 };
            int counter = 0;
            while (NotEqual(yourByte, compareByte))
            {
                yourByte = Subtraction(yourByte, divisorByte, byteBase);
                counter++;
            }
            return ReturnBytes(counter, byteBase);
        }

        public byte[] ResizeByte(byte[] yourByte, int lenght)
        {
            Array.Reverse(yourByte);
            Array.Resize(ref yourByte, lenght);
            Array.Reverse(yourByte);
            return yourByte;
        }

        public int GetAt(byte[] yourByte, int i)
        {
            return (i >= yourByte.Length ? 0 : yourByte[yourByte.Length - i - 1]);
        }

        public byte[] EraseZeros(byte[] yourByte)
        {
            int counter = 0;
            for (int i = 0; i < yourByte.Length; i++)
            {
                if (yourByte[i] == 0)
                    counter++;
                else
                    break;
            }
            return ResizeByte(yourByte, (yourByte.Length == counter ? 1 : yourByte.Length - counter));
        }
    }
}