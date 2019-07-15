using BattleshipApp.Data.IRepo;
using BattleshipApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipApp.Data.Repo
{
    public class PlayerRepository : IPlayerRepository
    {
        private IBoardRepository _boardRepository;

        public PlayerRepository(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public Player CreateNewPlayer(string name, FiringBoard firingBoard)
        {
            Player newPlayer = new Player(name, firingBoard);

            return newPlayer;
        }

        public string Shot(Player player, int row, char column)
        {
            Coordinates shotFired = new Coordinates(row, column);

            var result = _boardRepository.ProcessShot(player, shotFired);

            var isGameOver = _boardRepository.CheckGameOver(player);

            return result;
  
        }
    }
}
