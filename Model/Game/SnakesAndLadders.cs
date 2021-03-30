using Microsoft.Extensions.Options;
using Model.Configuration;
using Model.Data.Dice;
using Model.Data.DTO.Input;
using Model.Data.DTO.Output;
using Model.Data.Levels;
using Model.Data.Players;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Model.Game
{
    public class SnakesAndLadders : ISnakesAndLadders
    {
        private readonly ILevel _level;
        private readonly IDice _dice;
        private readonly IConfigLoader _configLoader;
        private readonly IOptions<SnakesAndLaddersConfig> _config;
        //could alternatively implement using MemoryCache with DI (production grade),
        //Since Memory/DistributedCache does not allow key enumartion - chose ConcurrentDictionary for simplicity.
        private ConcurrentDictionary<string, Player> players;

        public SnakesAndLadders(ILevel level, IDice dice, 
            IConfigLoader configLoader, IOptions<SnakesAndLaddersConfig> config)
        {
            _level = level;
            _dice = dice;
            _configLoader = configLoader;
            _config = config;
            _level.Initialize(_configLoader.Load(_config.Value.ConfigFileName));
            players = new ConcurrentDictionary<string, Player>();
        }

        public AddPlayerOutput AddPlayer(AddPlayerInput data)
        {
            var wasQueued = false;
            try
            {
                //Add playerTask to threadpool queue
                wasQueued = ThreadPool.QueueUserWorkItem((o) => AddPlayerTask(data));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new AddPlayerOutput() { PlayerID = data.PlayerID, Message = wasQueued ? "Success" : "Failure" };
        }

        public GetStatusOutput GetStatus(GetStatusInput data)
        {
            players.TryGetValue(data.PlayerID, out var player);
            if (player == null)
            {
                return new GetStatusOutput()
                {
                    HasHighestScore = false,
                    Score = 0,
                    Status = PlayerStatus.Unknown.ToString()
                };
            }
            return new GetStatusOutput()
            {
                Status = player.Status.ToString(),
                Score = player.CurrentScore,
                HasHighestScore = player.HighestScore
            };
        }

        private void AddPlayerTask(AddPlayerInput data)
        {
            //Assuming player can re-play with the same id 
            //Only last game score will persist in memory
            Debug.WriteLine("AddPlayerTask with ID: " + data.PlayerID);
            var player = new Player
            {
                PlayerID = data.PlayerID,
                PlayerName = data.PlayerName,
                CurrentPosition = 1,
                Status = PlayerStatus.Playing
            };

            while (player.Status.Equals(PlayerStatus.Playing))
            {
                _level.MovePlayer(player, _dice.Roll());
                lock (players)
                {
                    players.AddOrUpdate(player.PlayerID, player, (oldVal, newVal) => player);
                }
            }

            //game play ended
            if (HasHighestScore(player.PlayerID))
            {
                var leadPlayers = players.Values.Where(
                    v => v.HighestScore.Equals(true) &&
                    v.CurrentScore > player.CurrentScore).ToList();

                lock (players)
                {
                    //set HighestScore = false on the previous lead players
                    foreach(var lp in leadPlayers)
                    {
                        lp.HighestScore = false;
                        players.AddOrUpdate(lp.PlayerID, lp, (oldVal, newVal) => lp);
                    }
                    //set HighestScore = true on current lead player
                    player.HighestScore = true;
                    players.AddOrUpdate(player.PlayerID, player, (oldVal, newVal) => player);
                }
            }
        }

        private bool HasHighestScore(string playerID)
        {
            var bestScore = players.Values.Min().CurrentScore;
            return players[playerID].CurrentScore <= bestScore;
        }
    }
}
