using BattleshipApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipApp.Data.IRepo
{
    public interface IPlayerRepository
    {
        Player CreateNewPlayer(string name, FiringBoard firingBoard);
        string Shot(Player player, int row, char column);
    }
}
