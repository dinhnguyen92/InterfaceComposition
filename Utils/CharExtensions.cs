namespace InterfaceComposition.Utils
{
    public static class CharExtensions
    {
        public static bool IsVowel(this char c)
        {
            return "aeiou".Contains(char.ToLower(c));
        }
    }
}
