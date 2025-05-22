namespace Battleship.Tester
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var program = new Battleship.Board.Program();

            program._battleshipService.AddWarship(1, program._navalService.WarshipService.Find(WarshipType.Carrier), DataHelper.GeneratePostion("A1", "A5"));
            program._battleshipService.AddWarship(1, program._navalService.WarshipService.Find(WarshipType.Battleship), DataHelper.GeneratePostion("A6", "A9"));
            program._battleshipService.AddWarship(1, program._navalService.WarshipService.Find(WarshipType.Destroyer), DataHelper.GeneratePostion("B1", "B3"));
            program._battleshipService.AddWarship(1, program._navalService.WarshipService.Find(WarshipType.Submarine), DataHelper.GeneratePostion("B4", "B6"));
            program._battleshipService.AddWarship(1, program._navalService.WarshipService.Find(WarshipType.PatrolBoat), DataHelper.GeneratePostion("B7", "B8"));

            program._battleshipService.AddWarship(2, program._navalService.WarshipService.Find(WarshipType.Carrier), DataHelper.GeneratePostion("A1", "A5"));
            program._battleshipService.AddWarship(2, program._navalService.WarshipService.Find(WarshipType.Battleship), DataHelper.GeneratePostion("A6", "A9"));
            program._battleshipService.AddWarship(2, program._navalService.WarshipService.Find(WarshipType.Destroyer), DataHelper.GeneratePostion("B1", "B3"));
            program._battleshipService.AddWarship(2, program._navalService.WarshipService.Find(WarshipType.Submarine), DataHelper.GeneratePostion("B4", "B6"));
            program._battleshipService.AddWarship(2, program._navalService.WarshipService.Find(WarshipType.PatrolBoat), DataHelper.GeneratePostion("B7", "B8"));

            program.AutoTestMode = true;
            program.StartWar();

            DataHelper.PrintResult(program._sb.ToString());
        }
    }
}