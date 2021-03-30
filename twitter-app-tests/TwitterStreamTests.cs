using Microsoft.VisualStudio.TestTools.UnitTesting;
using twitter_data.Entities;
using twitter_data.Interface;
using twitter_data.Managers;
using Shouldly;

namespace twitter_app_tests
{
    [TestClass]
    public class TwitterStreamTests
    {
        private IAppStream _data;

        [TestInitialize]
        public void Init()
        {
            _data = new TwitterStream();
        }
    }
}
