using NUnit.Framework;

namespace NukedBit.Core.Tests
{
    public class StringExtensions
    {
    

        [Test]
        public void IsNullOrEmpty()
        {
            Assert.True("".IsEmptyOrNull());
        }
    }
}