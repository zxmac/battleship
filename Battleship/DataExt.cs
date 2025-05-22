namespace Battleship
{
    public static class DataExt
    {
        public static int ToNum(this string s)
        {
            int.TryParse(s, out int n);
            return n;
        }
        public static char ToLetNum(this int i) => (char)(i + 64);
        public static int ToPosY(this string position) => char.ToUpper(position[0]) - 64;
        public static int ToPosX(this string position) => position[1..].ToNum();
    }
}
