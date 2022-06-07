using System.Text;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Ukulele;
using Ukulele.Controllers.Alesis;
using Ukulele.PiShock;
using static Ukulele.MathExtensions;

try
{
    EnvExtensions.TryLoadFile(".env");
    var username = EnvExtensions.GetOrThrow("USERNAME");
    var apiKey = EnvExtensions.GetOrThrow("API_KEY");
    var code = EnvExtensions.GetOrThrow("CODE");
    var name = EnvExtensions.GetOrThrow("NAME");
    var alesisController = new AlesisController();
    var client = new PiShockClient(username, apiKey, code, name);
    var random = new Random();

    var inputs = InputDevice.GetAll().ToArray();
    Console.WriteLine($"Found {inputs.Length} inputs:\n{string.Join("\n", inputs.Select(input => input.Name))}");

    InputDevice input;
    switch (inputs.Length)
    {
        case 0:
            Console.Error.WriteLine("No input devices found.");
            Environment.Exit(1);
            return;
        case 1:
            input = inputs[0];
            break;
        default:
            var builder = new StringBuilder();
            for (var i = 0; i < inputs.Length; i++)
            {
                builder.Append($"{i}: {inputs[i].Name}\n");
            }

            Console.WriteLine($"Choose an input device:\n{builder}");
            string inputString;
            int inputNumber;
            do
            {
                inputString = Console.ReadLine() ?? "";
            } while (!int.TryParse(inputString, out inputNumber));

            input = inputs[inputNumber];
            break;
    }

    Console.Clear();
    Console.WriteLine($"Selecting {input.Name}");
    AppDomain.CurrentDomain.ProcessExit += (sender, args) => { input.StopEventsListening(); };

    var thread = new Thread(() =>
    {
        input.EventReceived += async (_, args) =>
        {
            if (alesisController.Read(args.Event))
            {
                Console.WriteLine(alesisController);
                return;
            }

            if (args.Event is not NoteOffEvent)
            {
                return;
            }

            var duration = MapSevenBitNumberTo(alesisController.Duration, 1, 15);
            var minimumWarning = MapSevenBitNumberTo(alesisController.MinimumWarning, 2, 10);
            var maximumWarning = MapSevenBitNumberTo(alesisController.MaximumWarning, 2, 10);
            var intensity = MapSevenBitNumberTo(alesisController.Intensity, 0, 100);

            if (minimumWarning + maximumWarning > 0)
            {
                if (minimumWarning > maximumWarning)
                {
                    (minimumWarning, maximumWarning) = (maximumWarning, minimumWarning);
                }

                var warning = random.Next(minimumWarning, maximumWarning);

                Console.WriteLine($"Warning for {warning} seconds with {intensity} intensity");

                client.Vibrate(duration, intensity);
                await Task.Delay(warning * 1000);
            }

            Console.WriteLine($"Shocking for {duration} seconds with {intensity} intensity");
            client.Shock(duration, intensity);
        };
        input.StartEventsListening();
    });

    thread.IsBackground = true;
    thread.Start();

    Console.ReadKey();
}
catch (Exception e)
{
    Console.Error.WriteLine(e);
    Console.ReadKey();
}
