using Battleship.Models;

namespace Battleship.IService
{
    public interface IServiceBase<T> where T : ModelBase
    {
        T MyDto { get; set; }
    }
}
