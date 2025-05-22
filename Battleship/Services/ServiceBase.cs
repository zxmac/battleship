using Battleship.Models;

namespace Battleship.Services
{
    public class ServiceBase<T> where T : ModelBase
    {
        public T MyDto { get; set; }
    }
}
