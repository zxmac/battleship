using Battleship.Models;

namespace Battleship.Services
{
    public class WarshipService : ServiceBase<Warship>
    {
        public readonly List<Warship> Warships;
        public WarshipService()
        {
            Warships = [];
        }
        public void Map(int id, int size, int missiles, HashSet<string> position)
        {
            MyDto = new Warship
            {
                Id = id,
                Name = ((WarshipType)id).ToString(),
                Size = size,
                Missiles = missiles,
                Position = position,
                PositionSize = size - 1,
                Strikes = []
            };
        }
        public void Create(WarshipType id, int size, int missiles)
        {
            Map((int)id, size, missiles, []);
            Warships.Add(MyDto);
        }
        public Warship Find(WarshipType id)
        {
            return Find((int)id);
        }
        public Warship Find(string id)
        {
            return Find(id.ToNum());
        }
        public Warship Find(int id)
        {
            return Warships.First(x => x.Id == id);
        }
        public bool IsWarshipExist(string id)
        {
            return Warships.Any(x => (int)x.Id == id.ToNum());
        }
    }
}
