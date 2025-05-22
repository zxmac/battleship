using Battleship.Models;

namespace Battleship.IService
{
    public interface IWarshipService : IServiceBase<Warship>
    {
        List<Warship> Warships { get; }
        void Create(WarshipType id, int size, int missiles);
        Warship Find(int id);
        Warship Find(string id);
        Warship Find(WarshipType id);
        bool IsWarshipExist(string id);
        void Map(int id, int size, int missiles, HashSet<string> position);
    }
}