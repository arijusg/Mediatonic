using System;
using Api.Interfaces;
using Api.Services;

namespace Api.Helpers
{
    public class TestableDateTime : ITestableDateTime
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
