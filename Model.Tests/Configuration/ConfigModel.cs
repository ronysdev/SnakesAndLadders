using System.Collections.Generic;
using Xunit;

namespace Model.Tests.Configuration
{
    public class ConfigModel
    {
        [Fact]
        public void ValidationTest()
        {
            var rules = new Dictionary<int, int>
            {
                { 1, 10 },
                { 5, 15 },
                { 20, 13 },
                { 30, 18 }
            };

            var configModel = new Model.Configuration.ConfigModel(rules, 50);
            Assert.True(configModel.Validate());
        }

        [Fact]
        public void OutOfBoundsValidationTest()
        {
            var rules = new Dictionary<int, int>
            {
                { 1, 10 },
                { 5, 15 },
                { 55, 13 }, // out of bounds
                { 30, 18 }
            };

            var configModel = new Model.Configuration.ConfigModel(rules, 50);
            Assert.False(configModel.Validate());
        }
    }
}
