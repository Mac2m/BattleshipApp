using System.ComponentModel;

namespace BattleshipApp.Data.Models
{
    public enum OccupationType
    {
        [Description("o")]
        Empty,
        [Description("M")]
        Miss,
        [Description("X")]
        Hit
    }
}