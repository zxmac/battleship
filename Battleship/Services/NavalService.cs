namespace Battleship.Services
{
    public class NavalService
    {
        private readonly WarshipService _warshipService;
        public WarshipService WarshipService => _warshipService;
        public NavalService()
        {
            _warshipService = new WarshipService();
        }
        public void CreateWarships()
        {
            _warshipService.Create(WarshipType.Carrier, 5, 25);
            _warshipService.Create(WarshipType.Battleship, 4, 25);
            _warshipService.Create(WarshipType.Destroyer, 3, 20);
            _warshipService.Create(WarshipType.Submarine, 3, 20);
            _warshipService.Create(WarshipType.PatrolBoat, 2, 10);
        }
    }
}
