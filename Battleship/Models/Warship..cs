namespace Battleship.Models
{
    public class Warship : ModelBase
    {
        public required string Name { get; set; }
        public int Size { get; set; }
        public required HashSet<string> Position {  get; set; }
        public int Missiles { get; set; }
        public int PositionSize { get; set; }
        public required HashSet<KeyValuePair<string, bool>> Strikes { get; set; }
    }
}
