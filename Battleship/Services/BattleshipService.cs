using Battleship.IService;
using Battleship.Models;

namespace Battleship.Services
{
    public class BattleshipService : IBattleshipService
    {
        private readonly IFleetService _fleetService;
        public IFleetService FleetService => _fleetService;
        public BattleshipService()
        {
            _fleetService = new FleetService();
        }
        public void CreateFleets()
        {
            _fleetService.Create(1);
            _fleetService.Create(2);
        }
        public void AddWarship(int id, Warship warship, HashSet<string> position)
        {
            _fleetService.WarshipService.Map(warship.Id, warship.Size, warship.Missiles, position);
            _fleetService.AddWarship(id, _fleetService.WarshipService.MyDto);
        }
        public bool HasWinner()
        {
            return _fleetService.Fleets.Any(f => f.Warships.SelectMany(x => x.Strikes).Count(x => x.Value) == 5);
        }
        public Fleet GetWinner()
        {
            return _fleetService.Fleets.First(f => f.Warships.SelectMany(x => x.Strikes).Count(x => x.Value) == 5);
        }
    }
}
