namespace Refactor_Switch_Statements.Extensions
{
    public static class Extension
    {
        public static bool In<T>(this T value, params T[] values) => values.Contains(value);
    }
}
