namespace BattleshipApp.Data.Models.Ships
{
    public class BattleShip : Ship
    {
        public BattleShip()
        {
            Name = "BattleShip";
            Width = 5;
            ShipType = ShipType.BattleShip;
        }
    }
}