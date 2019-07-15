using BattleshipApp.Data.IRepo;
using BattleshipApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipApp.Data.Repo
{
    public class BoardRepository : IBoardRepository
    {
        private FiringBoard _firingBoard;

        public FiringBoard SetUp()
        {
            _firingBoard = new FiringBoard();

            PlaceShipsRandomly();

            return _firingBoard;
        }

        public void PlaceShipsRandomly()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int shipNumber = 1;
            foreach (var ship in _firingBoard.Ships)
            {
                bool isOpen = true;
                
                ship.Number = shipNumber;
                shipNumber++;

                while (isOpen)
                {
                    char startcolumn = (char)rand.Next(65, 74);
                    var startrow = rand.Next(1, 11);
                    int endrow = startrow;
                    char endcolumn = startcolumn;
                    var orientation = rand.Next(1, 101) % 2;

                    List<int> panelNumbers = new List<int>();
                    if (orientation == 0)
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endrow++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endcolumn++;
                        }
                    }

                    if (endrow > 10 || endcolumn > 'J')
                    {
                        isOpen = true;
                        continue;
                    }

                    var affectedPanels = _firingBoard.Fields.Where(x => x.Coordinates.Row >= startrow
                                    && x.Coordinates.Column >= startcolumn
                                    && x.Coordinates.Row <= endrow
                                    && x.Coordinates.Column <= endcolumn).ToList();
                    if (affectedPanels.Any(x => x.IsOccupiedByShip))
                    {
                        isOpen = true;
                        continue;
                    }

                    foreach (var panel in affectedPanels)
                    {
                        panel.ShipType = ship.ShipType;
                        panel.ShipNumber = ship.Number;
                    }
                    isOpen = false;
                }
            }
        }

        public string ProcessShot(Player player, Coordinates coords)
        {
            var field = player.FiringBoard.Fields.Where(x => x.Coordinates.Row == coords.Row && x.Coordinates.Column == coords.Column).First();

            if (field.OccupationType == OccupationType.Empty && !field.IsOccupiedByShip)
            {
                field.OccupationType = OccupationType.Miss;
                return "Miss!";
            }
            else if (field.IsOccupiedByShip)
            {
                var ship = _firingBoard.Ships.First(x => x.ShipType == field.ShipType && x.Number == field.ShipNumber);
                ship.Hits++;
                field.OccupationType = OccupationType.Hit;
                if (ship.IsSunk)
                    return "Hit! It's sunken!";

                return "Hit!";
            }
            else
                return "Cannot fire there Captain!";
                
        }

        public bool CheckGameOver(Player player)
        {
            return player.FiringBoard.GameOver;
        }
    }
}