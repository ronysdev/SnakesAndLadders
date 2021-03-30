using Model.Configuration;
using Model.Data.Players;

namespace Model.Data.Levels
{
    public class Level : ILevel
    {
        private ConfigModel configuration;

        public Level() {}

        public void Initialize(ConfigModel configModel)
        {
            configuration = configModel;
        }
        public void MovePlayer(Player player, int steps)
        {
            if (!ValidatePlayer(player) || steps.Equals(0))
                return;
            var nextPosition = player.CurrentPosition + steps;
            player.CurrentScore += 1;
            if (nextPosition >= configuration.Size)
            {
                //finished
                player.CurrentPosition = configuration.Size;
                player.Status = PlayerStatus.Finished;
                return;
            }
            if (configuration.Rules.ContainsKey(nextPosition))
                nextPosition = configuration.Rules[nextPosition];
            player.CurrentPosition = nextPosition;
        }
        private bool ValidatePlayer(Player player)
        {
            return !string.IsNullOrEmpty(player.PlayerID) 
                && player.Status.Equals(PlayerStatus.Playing);
        }

    }
}
