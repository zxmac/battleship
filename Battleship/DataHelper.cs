namespace Battleship
{
    public static class DataHelper
    {
        public static HashSet<string> GeneratePostion(string posBow, string posStern)
        {
            return GeneratePostion(posBow.ToPosY(), posBow.ToPosX(), posStern.ToPosY(), posStern.ToPosX());
        }
        public static HashSet<string> GeneratePostion(int positionY, int positionX, int position2Y, int position2X)
        {
            var diffY = positionY > position2Y ? positionY - position2Y : position2Y - positionY;
            var diffX = positionX > position2X ? positionX - position2X : position2X - positionX;
            var positionList = new HashSet<string>();

            if (diffY == 0)
            {
                for (var ii = 0; ii <= diffX; ii++)
                {
                    if (positionX > position2X)
                        positionList.Add($"{positionY.ToLetNum()}{positionX--}");
                    else
                        positionList.Add($"{positionY.ToLetNum()}{positionX++}");
                }
            }
            else if (diffX == 0)
            {
                for (var ii = 0; ii <= diffY; ii++)
                {
                    if (positionY > position2Y)
                        positionList.Add($"{(positionY--).ToLetNum()}{positionX}");
                    else
                        positionList.Add($"{(positionY++).ToLetNum()}{positionX}");
                }
            }
            else throw new Exception("Something is wrong!");

            return positionList;
        }
        public static void PrintResult(string s)
        {
            var dir = Directory.GetCurrentDirectory();
            var folderPath = Path.GetFullPath(Path.Combine(dir, @"..\..\..\.."));
            var path = Path.Combine(folderPath, $"Battleship.Tester\\Result\\battleship_result_{DateTime.Now:yyyyMMddhhmmsss}.txt");
            File.WriteAllText(path, s);
        }
    }
}
