using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Api.Tests
{
    public class MockHelper
    {
        private IDictionary<Type, Mock> _mocks;

        public MockHelper()
        {
            _mocks = new ConcurrentDictionary<Type, Mock>();
        }

        public Mock<T> MockOut<T>() where T : class
        {
            if (_mocks.ContainsKey(typeof(T)))
            {
                RebindToConfiguredMocks();
                return (Mock<T>)_mocks[typeof(T)];
            }

            _mocks.Add(typeof(T), new Mock<T>());
            RebindToConfiguredMocks();

            return (Mock<T>)_mocks[typeof(T)];
        }

        private void RebindToConfiguredMocks()
        {
            foreach (var item in _mocks)
            {
                Startup.Container.Unbind(item.Key);
                var item1 = item;
                Startup.Container.Bind(item.Key).ToMethod(_ => _mocks[item1.Key].Object);
            }
        }
    }
}
