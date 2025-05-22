# Battleship
Battleship Take Home Coding Exam

### Specifiction
- This battleship game is based on 2002 version.
- The game can handle 2 players and each player can have fleet with 5 warships.
- Each warships has its own missiles. Total missiles of each fleet warships is 100.
- A warship can only fire missiles if missiles is greater than 0.
- In-game - If a player hits a warship, the opponent warship will be identified and message will be displayed on the console.
- All console inputs has input validator(s). A player may encounter a console input message with '(Please enter a valid input):' as a warning.
- On initial run of the game - the players can select whether they start normally to assemble their fleet, or start directly to begin the war.
- All logs are visible on the console and it can also print result of the game.


### Running battleship using console app
1. Locate Battleship.Board project
2. Right-click the project
3. Click -> 'Set as Startup Project'
4. Run the project
- On success run -> Game result will be printed to Battleship.Tester/Result/


### Initial game guide
If 'Start battleship' is selected by pressing 'Enter' or any key other than 'W'
- All fleets warships will be manually entered

If 'Start war' is select by pressing 'W'.
- All fleets & warships are pre-assembled


### War game input guide
Console> Select warship [1-Carrier-25|2-Battleship-25|3-Destroyer-20|4-Submarine-20|5-PatrolBoat-2]:
- Format sample: '1-Carrier-25' -> 1 == WarshipId, Carrier == WarshipName; 25 == Missiles left
- Player can directly press 'Enter' to get a random warship

Console> Enter Carrier strike position:
- Sample -> 'A1'


### Running battleship using unit-test
1. Locate Battleship.Tester project
2. Open UnitTest1.cs
3. Right-click -> '[Fact]' on the 'Test1' method
4. Click -> 'Debug Tests'


### Unit-test guide
- If AutoTestMode is enabled - The war will be automated
- On success run -> Game result will be printed to Battleship.Tester/Result/

