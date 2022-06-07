namespace Ukulele;

public static class Logger
{
    private static CancellationTokenSource _warningCancel = new();

    private static void PrintWarning(char symbol, int seconds, int intensity)
    {
        Console.SetCursorPosition(0, 5);
        Console.WriteLine($"{symbol} Warning for {seconds} seconds with {intensity} intensity");
    }
    
    public static async Task PrintWarningFor(int seconds, int intensity)
    {
        _warningCancel.Cancel();
        _warningCancel = new CancellationTokenSource();
        
        var cancel = _warningCancel;
        var thread = new Thread(() =>
        {
            var warningSymbol = ' ';

            while (true)
            {
                if (cancel.Token.IsCancellationRequested)
                {
                    return;
                }

                warningSymbol = ConsoleExtensions.NextLoadingSymbol(warningSymbol);
                PrintWarning(warningSymbol, seconds, intensity);
                Thread.Sleep(100);
            }
        });
        
        thread.Start();

        await Task.Delay(seconds * 1000, CancellationToken.None);

        if (!cancel.IsCancellationRequested)
        {
            cancel.Cancel();
            PrintWarning('✓', seconds, intensity);
        }
    }
}