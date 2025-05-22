using Battleship.Models;

namespace Battleship.IService
{
    public interface IBattleshipService
    {
        IFleetService FleetService { get; }

        void AddWarship(int id, Warship warship, HashSet<string> position);
        void CreateFleets();
        Fleet GetWinner();
        bool HasWinner();
    }
}