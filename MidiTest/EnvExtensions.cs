namespace Ukulele;

public class EnvExtensions
{
    public static string GetOrThrow(string name)
    {
        return Environment.GetEnvironmentVariable(name) ??
               throw new NullReferenceException($"No environment variable found with name {name}");
    }

    public static void TryLoadFile(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }

        foreach (var line in File.ReadLines(path))
        {
            var split = line.Split('=');
            Environment.SetEnvironmentVariable(split[0], split[1]);
        }
    }
}
