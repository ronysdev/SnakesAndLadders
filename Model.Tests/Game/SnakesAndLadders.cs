using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Model.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Model.Tests.Game
{
    public class SnakesAndLadders
    {
        [Fact]
        public void AddPlayerTest()
        {
            var level = new Model.Data.Levels.Level();
            var rules = new Dictionary<int, int>
            {
                { 1, 10 },
                { 5, 15 },
                { 20, 13 },
                { 30, 18 }
            };
            var configModel = new Model.Configuration.ConfigModel(rules, 50);

            var hostEnvMock = new Mock<IHostEnvironment>();
            hostEnvMock
                .Setup(m => m.ContentRootPath)
                .Returns(AppDomain.CurrentDomain.BaseDirectory);

            var optionsMock = Options.Create(new SnakesAndLaddersConfig() { ConfigFileName = "levelConfig.json" });

            var loader = new JsonConfigLoader(hostEnvMock.Object, optionsMock);

            var game = new Model.Game.SnakesAndLadders(level, new Model.Data.Dice.Dice(), loader, optionsMock);
            Assert.NotNull(game.AddPlayer(new Model.Data.DTO.Input.AddPlayerInput { PlayerID = "1", PlayerName = "Ron" }));

            Thread.Sleep(1000);

            Assert.True(game.GetStatus(new Model.Data.DTO.Input.GetStatusInput { PlayerID = "1" }).HasHighestScore); //single player must have highest score

            Assert.NotNull(game.AddPlayer(new Model.Data.DTO.Input.AddPlayerInput { PlayerID = "1", PlayerName = "Ron" })); //player can play twice

            Thread.Sleep(1000);

            Assert.NotEqual(
                game.GetStatus(new Model.Data.DTO.Input.GetStatusInput { PlayerID = "1" }).Status,
                Model.Data.Players.PlayerStatus.Unknown.ToString());

            Assert.Equal(
                game.GetStatus(new Model.Data.DTO.Input.GetStatusInput { PlayerID = "5" }).Status,
                Model.Data.Players.PlayerStatus.Unknown.ToString()); //unknown player


        }
    }
}
