namespace Battleship.Models
{
    public class Fleet : ModelBase
    {
        public required string Name { get; set; }
        public int OpponentId { get; set; }
        public required List<Warship> Warships { get; set; }
    }
}
