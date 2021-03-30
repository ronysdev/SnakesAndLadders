using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Model.Configuration;
using Moq;
using System;
using Xunit;

namespace Model.Tests.Configuration
{
    public class JsonConfigLoader
    {
        [Fact]
        public void LoadTest()
        {
            var hostEnvMock = new Mock<IHostEnvironment>();
            hostEnvMock
                .Setup(m => m.ContentRootPath)
                .Returns(AppDomain.CurrentDomain.BaseDirectory);

            var optionsMock = Options.Create(new SnakesAndLaddersConfig() { ConfigFileName = "levelConfig.json" });

            var loader = new Model.Configuration.JsonConfigLoader(hostEnvMock.Object, optionsMock);
            Assert.NotNull(loader.Load("levelConfig.json"));
        }
    }
}
