using BattleshipApp.Data.Models;
using BattleshipApp.Data.Models.IRepo;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                throw eventArgs.Exception;
            };

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var boardRepository = kernel.Get<IBoardRepository>();
            var playerRepository = kernel.Get<IPlayerRepository>();

            var board = boardRepository.SetUp();
            Console.WriteLine("Hello Captain! What's your name?");
            var playerName = Console.ReadLine();
            Console.WriteLine($"Aye { playerName } !");
            var player = playerRepository.CreateNewPlayer(playerName, board);
            DrawBoard(board);

            while (!boardRepository.CheckGameOver(player))
            {
                Console.WriteLine("Captain, where do you want to shot?");
                Console.WriteLine("Choose coordinates");

                string coords = Console.ReadLine().ToUpper();
                if (!string.IsNullOrEmpty(coords))
                {
                    TakeAShot(playerRepository, player, coords);
                    DrawBoard(board);
                }
                else
                    Console.Write("Choose proper coordinates");
                
            }

            Console.Write("All enemy ships are sunken, you win! Game is over.");
            Console.ReadKey();
        }

        private static void DrawBoard(FiringBoard board)
        {
            Console.WriteLine("    A B C D E F G H I J ");
            Console.WriteLine("   _____________________");
            for (int row = 1; row <= 9; row++)
            {
                Console.Write(row + "  ¦");

                for (char column = 'A'; column <= 'J'; column++)
                {
                    Console.Write(board.Fields.Where(x => x.Coordinates.Row == row && x.Coordinates.Column == column).First().Status + " ");
                }

                Console.WriteLine(Environment.NewLine + "   ¦");
            }
            Console.Write("10 ¦");
            for (char column = 'A'; column <= 'J'; column++)
            {
                Console.Write(board.Fields.Where(x => x.Coordinates.Row == 10 && x.Coordinates.Column == column).First().Status + " ");
            }
            Console.WriteLine(Environment.NewLine);
        }

        private static void TakeAShot(IPlayerRepository playerRepository, Player player, string coords)
        {
            int coordRow;
            int.TryParse(coords.Substring(1), out coordRow);
            char coordColumn;
            char.TryParse(coords.Substring(0, 1), out coordColumn);

            if ((coordRow > 10 || coordColumn > 'J') || (coordRow < 1 || coordColumn < 'A'))
                Console.WriteLine("Choose correct coordinates.");
            else
            {
                string result = playerRepository.Shot(player, coordRow, coordColumn);
                Console.Write(result);
                Console.WriteLine(Environment.NewLine);
            }
            
        }
    }
}
