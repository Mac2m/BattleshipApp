using System.Collections.Generic;

namespace BattleshipApp.Data.Models
{
    public class Player
    {
        public string Name { get; set; }
        public FiringBoard FiringBoard { get; set; }

        public Player(string name, FiringBoard firingBoard)
        {
            Name = name;
            FiringBoard = firingBoard;
        }

    }
}