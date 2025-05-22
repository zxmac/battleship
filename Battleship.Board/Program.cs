using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using Battleship.Models;
using Battleship.Services;

new Battleship.Board.Program().Execute();

namespace Battleship.Board
{
    public class Program
    {
        public readonly NavalService _navalService;
        public readonly BattleshipService _battleshipService;

        public bool AutoTestMode { get; set; }
        public readonly StringBuilder _sb = new();

        public Program()
        {
            _navalService = new NavalService();
            _navalService.CreateWarships();
            _battleshipService = new BattleshipService();
            _battleshipService.CreateFleets();
        }
        public void Execute()
        {
            Log("Press -> [Enter] Start battleship | [W] Start war: ");
            var input = Console.ReadLine();

            if (input.ToUpper() != "W")
            {
                Process();
            }
            else
            {
                _battleshipService.AddWarship(1, _navalService.WarshipService.Find(WarshipType.Carrier), DataHelper.GeneratePostion("A1", "A5"));
                _battleshipService.AddWarship(1, _navalService.WarshipService.Find(WarshipType.Battleship), DataHelper.GeneratePostion("A6", "A9"));
                _battleshipService.AddWarship(1, _navalService.WarshipService.Find(WarshipType.Destroyer), DataHelper.GeneratePostion("B1", "B3"));
                _battleshipService.AddWarship(1, _navalService.WarshipService.Find(WarshipType.Submarine), DataHelper.GeneratePostion("B4", "B6"));
                _battleshipService.AddWarship(1, _navalService.WarshipService.Find(WarshipType.PatrolBoat), DataHelper.GeneratePostion("B7", "B8"));

                _battleshipService.AddWarship(2, _navalService.WarshipService.Find(WarshipType.Carrier), DataHelper.GeneratePostion("A1", "A5"));
                _battleshipService.AddWarship(2, _navalService.WarshipService.Find(WarshipType.Battleship), DataHelper.GeneratePostion("A6", "A9"));
                _battleshipService.AddWarship(2, _navalService.WarshipService.Find(WarshipType.Destroyer), DataHelper.GeneratePostion("B1", "B3"));
                _battleshipService.AddWarship(2, _navalService.WarshipService.Find(WarshipType.Submarine), DataHelper.GeneratePostion("B4", "B6"));
                _battleshipService.AddWarship(2, _navalService.WarshipService.Find(WarshipType.PatrolBoat), DataHelper.GeneratePostion("B7", "B8"));
            }

            LogLine(">< Let's start the war !!! ><");

            StartWar();

            DataHelper.PrintResult(_sb.ToString());

            Console.ReadLine();
        }
        public void Process()
        {
            LogLine(" No.\t| Class of ship\t\t\t| Size\t|");
            _navalService.WarshipService.Warships.ForEach(warship =>
            {
                LogLine($" {(warship.Id)}\t| {warship.Name}\t\t\t| {warship.Size}\t|");
            });
            LogLine();

            foreach (var fleet in _battleshipService.FleetService.Fleets)
            {
                LogLine($">>--Player {fleet.Id} assemble your fleet--<<");

                for (var j = 0; j < _navalService.WarshipService.Warships.Count; j++)
                {
                    var warshipInput = ValidateInput(input => input.ToNum() != 0
                        && _navalService.WarshipService.IsWarshipExist(input)
                        && !_battleshipService.FleetService.IsWarshipExist(fleet.Id, input), 
                        $"Please select warship({j + 1})");
                    var selectedWarship = _navalService.WarshipService.Find(warshipInput);

                    var positionInput = ValidateInput(input => PositionInputValidator(input)
                        && !_battleshipService.FleetService.IsPositionExist(fleet.Id, input), 
                        $"Enter the {selectedWarship.Name} bow position");

                    var positionY = positionInput.ToPosY();
                    var positionX = positionInput.ToPosX();

                    List<Tuple<string, int, int, bool>>  positionList =
                    [
                        new("North", positionY - selectedWarship.PositionSize, positionX, (positionY - selectedWarship.PositionSize) >= 1),
                        new("West", positionY, positionX + selectedWarship.PositionSize, (positionX + selectedWarship.PositionSize) <= 10),
                        new("East", positionY, positionX - selectedWarship.PositionSize, (positionX - selectedWarship.PositionSize) >= 1),
                        new("South", positionY + selectedWarship.PositionSize, positionX, (positionY + selectedWarship.PositionSize) <= 10),
                    ];

                    var availablePositions = positionList.Where(x => x.Item4)
                        .Where(position => !_battleshipService.FleetService.IsPositionExist(fleet.Id, $"{position.Item2.ToLetNum()}{position.Item3}"))
                        .Select(x => $"{x.Item1}: {x.Item2.ToLetNum()}{x.Item3}").ToList();

                    LogLine($"Available stern positions for bow {positionInput} -> [{string.Join(" | ", availablePositions)}]");

                    var position2Input = ValidateInput(input => 
                        positionList.Any(x => $"{x.Item2.ToLetNum()}{x.Item3}" == input), 
                        $"Enter the {selectedWarship.Name} stern position");

                    var position2Y = position2Input.ToPosY();
                    var position2X = position2Input.ToPosX();
                    var warshipPosition = DataHelper.GeneratePostion(positionY, positionX, position2Y, position2X);

                    _battleshipService.AddWarship(fleet.Id, selectedWarship, warshipPosition);

                    LogLine($"{selectedWarship.Name} position <={string.Join("=", warshipPosition.Select(x => x))}=>>");
                }
            }
        }
        public void StartWar()
        {
            if (_battleshipService.FleetService.Fleets.Count(fleet => fleet.Warships.Count == 5) != 2)
                throw new Exception("It must have 2 fleets with 5 warships each fleet!");

            while (!_battleshipService.HasWinner())
            {
                for (var i = 1; i <= _battleshipService.FleetService.Fleets.Count; i++)
                {
                    var myWarshipInput = string.Empty;
                    Warship? onAttackWarship = null;
                    var opponentFleetId = i == 1 ? 2 : 1;
                    var strikePostion = string.Empty;

                    if (!AutoTestMode)
                    {
                        var warshipsInfo = string.Join("|", _battleshipService.FleetService
                            .Find(i).Warships.Select(x => $"{x.Id}-{x.Name}-{x.Missiles}").ToArray());

                        myWarshipInput = ValidateInput(input => string.IsNullOrEmpty(input) 
                            || (input.ToNum() != 0 && _battleshipService.FleetService.IsWarshipAvailablet(i, input)), 
                            $"[Player-{i}] Select warship [{warshipsInfo}]");

                        if (string.IsNullOrEmpty(myWarshipInput))
                            onAttackWarship = GetRandomWarship(i);
                        else
                            onAttackWarship = _battleshipService.FleetService.GetWarship(i, myWarshipInput);

                        strikePostion = ValidateInput(input => PositionInputValidator(input)
                            && !_battleshipService.FleetService.IsStrikeExist(i, input), 
                            $"[Player-{i}] Enter {onAttackWarship.Name} strike position");
                    }
                    else
                    {
                        onAttackWarship = GetRandomWarship(i);

                        while (true)
                        {
                            strikePostion = RandomPosition();
                            if (!_battleshipService.FleetService.IsStrikeExist(i, strikePostion)) break;
                        }
                    }
                    
                    var warshipTarget = _battleshipService.FleetService.GetWarshipByPosition(opponentFleetId, strikePostion);                    
                    onAttackWarship.Strikes.Add(new(strikePostion, warshipTarget != null));
                    onAttackWarship.Missiles -= 1;

                    LogLine($"[Player-{i}] Strike position {strikePostion} {(warshipTarget != null ? $"opponent {warshipTarget.Name} hit" : "missed")}!");
                    LogLine($"[Player-{i}] Hit <{_battleshipService.FleetService.GetHitCount(i)}> " +
                        $"| Missed <{_battleshipService.FleetService.GetMissedCount(i)}> " +
                        $"| Missiles <{_battleshipService.FleetService.GetMissileCount(i)}>");

                    if (_battleshipService.HasWinner()) continue;
                }
            }

            var fleet = _battleshipService.GetWinner();
            LogLine($"***** >>--Winner {fleet.Name}--<< *****");
        }


        private Warship GetRandomWarship(int id)
        {
            var rndId = string.Empty;
            Warship? warship = null;
            while (!(warship?.Missiles > 0))
            {
                rndId = RandomWarshipId().ToString();
                warship = _battleshipService.FleetService.GetWarship(id, rndId);
            }
            return warship;
        }
        private void Log(string s)
        {
            Console.Write(s);
            _sb.AppendLine(s);
        }
        private void LogLine(string s = "")
        {
            Console.WriteLine(s);
            _sb.AppendLine(s);
        }
        private int RandomWarshipId()
        {
            Random rnd = new Random();
            return rnd.Next((int)_navalService.WarshipService.Warships.First().Id, (int)_navalService.WarshipService.Warships.Last().Id + 1);
        }
        private string RandomPosition()
        {
            Random rnd = new Random();
            var posY = rnd.Next(1, 11);
            var posX = rnd.Next(1, 11);
            return $"{posY.ToLetNum()}{posX}";
        }
        private bool PositionInputValidator(string input)
        {
            return input.Length >= 2
                && Regex.IsMatch(input[0].ToString(), @"^[a-zA-Z]+$")
                && Regex.IsMatch(input[1].ToString(), @"^[0-9]+$")
                && input.ToPosY() <= 10 && input.ToPosX() <= 10;
        }
        private string ValidateInput(Expression<Func<string, bool>> exp, string msg)
        {
            string? input = null;
            var ctr = 0;
            while (ctr == 0 || !exp.Compile()(input))
            {
                if (ctr == 1) msg += " (Please enter a valid input)";
                Log(msg + ": ");
                input = Console.ReadLine();
                _sb.AppendLine(input);
                ctr++;
            }
            return input;
        }
    }
}

