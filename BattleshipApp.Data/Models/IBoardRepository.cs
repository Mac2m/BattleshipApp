using System.Collections.Generic;

namespace BattleshipApp.Data.Models
{
    public interface IBoardRepository
    {
        FiringBoard SetUp();
        void PlaceShipsRandomly();
        string ProcessShot(Player player, Coordinates coords);
        bool CheckGameOver(Player player);

    }
}