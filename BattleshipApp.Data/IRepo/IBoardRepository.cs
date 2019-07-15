using BattleshipApp.Data.Models;
using System.Collections.Generic;

namespace BattleshipApp.Data.IRepo
{
    public interface IBoardRepository
    {
        FiringBoard SetUp();
        void PlaceShipsRandomly();
        string ProcessShot(Player player, Coordinates coords);
        bool CheckGameOver(Player player);

    }
}