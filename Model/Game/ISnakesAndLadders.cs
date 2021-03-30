using Model.Data.DTO.Input;
using Model.Data.DTO.Output;

namespace Model.Game
{
    public interface ISnakesAndLadders
    {
        AddPlayerOutput AddPlayer(AddPlayerInput data);
        GetStatusOutput GetStatus(GetStatusInput data);
    }
}
