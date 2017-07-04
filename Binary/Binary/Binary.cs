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
        [InlineData(1, 7, 2)]
        [InlineData(0, 6, 3)]
        public void IsGetAtGood(int returnedValue, int yourByte, int position)
        {
            Assert.Equal(returnedValue, GetAt(ReturnBytes(yourByte), position));
        }

        [Theory]
        [InlineData(0, 1)]
        public void GetNot(int first, int second)
        {
            Assert.Equal(ReturnBytes(first), NOT(ReturnBytes(second)));
        }

        [Theory]
        [InlineData(7, 5, 3)]
        public void GetOR(int expected, int first, int second)
        {
            Assert.Equal(ReturnBytes(expected), OR(ReturnBytes(first), ReturnBytes(second)));
        }

        [Theory]
        [InlineData(3, 7, 3)]
        public void GetAND(int expected, int first, int second)
        {
            Assert.Equal(ReturnBytes(expected), AND(ReturnBytes(first), ReturnBytes(second)));
        }

        [Theory]
        [InlineData(6, 5, 3)] 
        public void GetXOR(int expected, int first, int second)
        {
            Assert.Equal(ReturnBytes(expected), XOR(ReturnBytes(first), ReturnBytes(second)));
        }

        [Theory]
        [InlineData(8, 1, 3)]
        public void GetShiftLeft(int expected, int number, int step)
        {
            Assert.Equal(ReturnBytes(expected), ShiftLeft(ReturnBytes(number), step));
        }

        [Theory]
        [InlineData(1, 8, 3)]
        public void GetShiftRight(int expected, int number, int step)
        {
            Assert.Equal(ReturnBytes(expected), ShiftRight(ReturnBytes(number), step));
        }

        [Theory]
        [InlineData(1, 8)]
        public void GetLess(int question, int answer)
        {
            Assert.True(LessThen(ReturnBytes(question), ReturnBytes(answer)));
        }
        
        [Theory]
        [InlineData(9, 8)]
        public void GetLessThenFail(int question, int answer)
        {
            Assert.False(LessThen(ReturnBytes(question), ReturnBytes(answer)));
        }

        [Theory]
        [InlineData(4, 3)]
        public void GetGreaterThen(int question, int answer)
        {
            Assert.True(GreaterThen(ReturnBytes(question), ReturnBytes(answer)));
        }

        [Theory]
        [InlineData(5, 8)]
        public void GetGreaterThenFail(int question, int answer)
        {
            Assert.False(GreaterThen(ReturnBytes(question), ReturnBytes(answer)));
        }

        [Theory]
        [InlineData(8, 8)]
        public void GetEqual(int question, int answer)
        {
            Assert.True(Equal(ReturnBytes(question), ReturnBytes(answer)));
        }

        [Theory]
        [InlineData(5, 8)]
        public void GetNotEqual(int question, int answer)
        {
            Assert.True(NotEqual(ReturnBytes(question), ReturnBytes(answer)));
        }

        [Theory]
        [InlineData(4, 2, 2)]
        [InlineData(8, 5, 3)]
        public void GetSum(int result, int first, int second)
        {
            Assert.Equal(ReturnBytes(result), Sum(ReturnBytes(first), ReturnBytes(second)));
        }

        [Theory]
        [InlineData(6, 2, 3)]
        [InlineData(10, 5, 2)]
        public void GetProduct(int result, int multiplicand, int multiplier)
        {
            Assert.Equal(ReturnBytes(result), Product(ReturnBytes(multiplicand), multiplier));
        }

        [Theory]
        [InlineData(5, 8, 3)]
        public void GetSubtraction(int result, int minuend, int subtrahend)
        {
            Assert.Equal(ReturnBytes(result), Subtraction(ReturnBytes(minuend), ReturnBytes(subtrahend)));
        }

        [Theory]
        [InlineData(5, 15, 3)]
        public void GetDivision(int result, int dividend, int divisor)
        {
            Assert.Equal(ReturnBytes(result), Division(ReturnBytes(dividend), divisor));
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
            byte[] result = new byte[Math.Max(firstByte.Length, secondByte.Length)];
            for (int i = 0; i < result.Length; i++)
                if (GetAt(firstByte,i) == 1 && GetAt(secondByte, i) == 1)
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
       
        public bool GreaterThen(byte[] firstByte, byte[] secondByte)
        {
            if (firstByte.Length > secondByte.Length)
                return true;
            if (firstByte.Length < secondByte.Length)
                return false;
            for (int i = 0; i < firstByte.Length; i++)
                if (firstByte[i] > secondByte[i]) 
                    return true;
            return false;
        }

        public bool Equal(byte[] firstByte, byte[] secondByte)
        {
            if (firstByte.Length != secondByte.Length)
                return false;
            for (int i = 0; i < firstByte.Length; i++)
                if (firstByte[i] != secondByte[i])
                    return false;
            return true;
        }

        public bool NotEqual(byte[] firstByte, byte[] secondByte)
        {
            if (firstByte.Length != secondByte.Length)
                return true;
            for (int i = 0; i < firstByte.Length; i++)
                if (firstByte[i] != secondByte[i])
                    return true;
            return false;
        }

        public byte[] Sum(byte[] firstByte, byte[] secondByte)
        {
            byte[] result = new byte[Math.Max(firstByte.Length, secondByte.Length)];
            int counter = 0;
            for (int i = 0; i < result.Length; i++)
            {
                var sum = GetAt(firstByte, i) + GetAt(secondByte, i) + counter;
                result[i] = (byte) (sum % 2);
                counter = sum / 2;
            }
            if (counter == 1)
            {
                result = ResizeByte(result, result.Length+1);
                result[0] = (byte )counter;
            }
            return result;
        }

        public byte[] Product(byte[] yourByte, int multiplier)
        {
            byte[] result = new byte[yourByte.Length];
            while (multiplier != 0)
            {
                result = Sum(result, yourByte);
                multiplier--;
            }
            return EraseZeros(result);
        }

        public byte[] Subtraction(byte[] firstByte, byte[] secondBye)
        {
            byte[] result = new byte[Math.Max(firstByte.Length, secondBye.Length)];
            int counter = 0;
            for (int i = 0; i < result.Length; i++)
            {
                var dif = 2 + GetAt(firstByte, i) - GetAt(secondBye, i) - counter;
                result[result.Length - i - 1] = (byte)(dif % 2);
                counter = dif<=1 ? 1: 0;
            }
            return EraseZeros(result);
        }

        public byte[] Division(byte[] yourByte, int divisor)
        {
            byte[] result = new byte[yourByte.Length];
            while (divisor != 0)
            {
                result = Subtraction(result, yourByte);
                divisor--;
            }
            return EraseZeros(result);
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
                if(yourByte[i] == 0)
                    counter++;
                else
                    break;
            }
            return ResizeByte(yourByte, (yourByte.Length == counter ? 1 : yourByte.Length - counter));       
        }
    }
}
