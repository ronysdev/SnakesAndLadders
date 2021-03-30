using Model.Configuration;
using Model.Data.Players;

namespace Model.Data.Levels
{
    public interface ILevel
    {
        void Initialize(ConfigModel configModel);
        void MovePlayer(Player player, int steps);
    }
}
