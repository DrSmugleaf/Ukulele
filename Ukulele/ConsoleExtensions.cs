namespace Ukulele;

public static class ConsoleExtensions
{
    public static char NextLoadingSymbol(char symbol)
    {
        return symbol switch
        {
            '-' => '\\',
            '\\' => '|',
            '|' => '/',
            '/' => '-',
            _ => '-'
        };
    }
}