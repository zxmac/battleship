using Battleship.Services;

namespace Battleship.IService
{
    public interface INavalService
    {
        IWarshipService WarshipService { get; }

        void CreateWarships();
    }
}