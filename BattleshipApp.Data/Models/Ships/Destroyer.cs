namespace BattleshipApp.Data.Models.Ships
{
    public class Destroyer : Ship
    {
        public Destroyer()
        {
            Name = "Destroyer";
            Width = 4;
            ShipType = ShipType.Destroyer;
        }
    }
}