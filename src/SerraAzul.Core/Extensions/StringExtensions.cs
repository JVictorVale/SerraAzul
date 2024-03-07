namespace SerraAzul.Core.Extensions;

public static class StringExtensions
{
    public static string? SomenteNumeros(this string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : new string(value.Where(char.IsDigit).ToArray());
    }
}