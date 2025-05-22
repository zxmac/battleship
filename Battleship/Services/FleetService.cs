using Battleship.Models;

namespace Battleship.Services
{
    public class FleetService : ServiceBase<Fleet>
    {
        private readonly WarshipService _warshipService;
        public WarshipService WarshipService => _warshipService;
        public List<Fleet> Fleets { get; set; } = [];
        public FleetService()
        {
            _warshipService = new WarshipService();
        }
        public Fleet Find(int id) => Fleets.First(x => x.Id == id);
        public void Map(int id)
        {
            MyDto = new Fleet
            {
                Id = id,
                Name = "Player " + id,
                Warships = []
            };
        }
        public void Create(int id)
        {
            Map(id);
            Fleets.Add(MyDto);
        }
        public void AddWarship(int id, Warship warship)
        {
            Find(id).Warships.Add(warship);
        }
        public bool IsWarshipExist(int id, string warshipId)
        {
            return Find(id).Warships.Any(w => w.Id == warshipId.ToNum());
        }
        public bool IsWarshipAvailablet(int id, string warshipId)
        {
            return Find(id).Warships.Any(w => w.Id == warshipId.ToNum() && w.Missiles > 0);
        }
        public Warship GetWarship(int id, string warshipId)
        {
            return Find(id).Warships.First(w => (int)w.Id == warshipId.ToNum());
        }
        public Warship? GetWarshipByPosition(int id, string position)
        {
            return Find(id).Warships.FirstOrDefault(warship => warship.Position.Any(x => x == position.ToUpper()));
        }
        public bool IsPositionExist(int id, string position)
        {
            return Find(id).Warships.Any(w => w.Position.Any(x => x == position));
        }
        public bool IsStrikeExist(int id, string position)
        {
            return Find(id).Warships.Any(w => w.Strikes.Any(x => x.Key == position));
        }
        public int GetHitCount(int id)
        {
            return Find(id).Warships.SelectMany(x => x.Strikes).Count(x => x.Value);
        }
        public int GetMissedCount(int id)
        {
            return Find(id).Warships.SelectMany(x => x.Strikes).Count(x => !x.Value);
        }
        public int GetMissileCount(int id)
        {
            return Find(id).Warships.Sum(x => x.Missiles);
        }
    }
}
