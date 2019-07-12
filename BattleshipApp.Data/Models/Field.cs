using System.ComponentModel;
using BattleshipApp.Data.Extensions;

namespace BattleshipApp.Data.Models
{
    public class Field
    {
        public OccupationType OccupationType { get; set; }
        public ShipType ShipType { get; set; }
        public int ShipNumber { get; set; }
        public Coordinates Coordinates { get; set; }

        public Field(int row, char column)
        {
            Coordinates = new Coordinates(row, column);
            OccupationType = OccupationType.Empty;
            ShipType = ShipType.None;
        }

        public string Status
        {
            get { return OccupationType.GetAttributeOfType<DescriptionAttribute>().Description; }
        }

        public bool IsOccupiedByShip
        {
            get
            {
                return ShipType == ShipType.BattleShip
                    || ShipType == ShipType.Destroyer;
            }
        }

    }
}