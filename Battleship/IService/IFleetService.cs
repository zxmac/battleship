using Battleship.Models;

namespace Battleship.IService
{
    public interface IFleetService : IServiceBase<Fleet>
    {
        List<Fleet> Fleets { get; }
        IWarshipService WarshipService { get; }

        void AddWarship(int id, Warship warship);
        void Create(int id);
        Fleet Find(int id);
        int GetHitCount(int id);
        int GetMissedCount(int id);
        int GetMissileCount(int id);
        Warship GetWarship(int id, string warshipId);
        Warship? GetWarshipByPosition(int id, string position);
        bool IsPositionExist(int id, string position);
        bool IsStrikeExist(int id, string position);
        bool IsWarshipAvailablet(int id, string warshipId);
        bool IsWarshipExist(int id, string warshipId);
        void Map(int id);
    }
}