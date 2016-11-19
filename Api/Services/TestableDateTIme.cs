using System;

namespace Api.Services
{
    public interface ITestableDateTime
    {
        DateTime UtcNow();
    }

    public class TestableDateTime : ITestableDateTime
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
