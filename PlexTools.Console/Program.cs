using PlexTools.API;
using PlexTools.Console;

// Requester-class for handling API-calls. Requires a token for authentication.
// For security reasons we fetch the key from environment, and return an exception if it's not found.
HttpApiRequester requester = new(
    Environment.GetEnvironmentVariable("PLEX_TOKEN") ?? throw new InvalidOperationException(
        "Can't fetch user token from environment.\n Use export PLEX_TOKEN='USERTOKEN' to set it."
        )
    );

// We fetch the url from environment as well. 
// We mostly do this becuase the api includes the IP for the plex server.
string url = Environment.GetEnvironmentVariable("PLEX_URL") ?? throw new InvalidOperationException(
    "Can't fetch API url.\n Use export PLEX_URL='URL' to set it."
);

// Class for deserializing the json package we get from api.
MetadataDeserializer deserializer = new();

// File manager class for saving backups of metadata found on plex server.
JsonFileManager fileManager = new();

// Creates a directory for backups if none exists. Otherwise does nothing.
Directory.CreateDirectory("./Backup");

// To avoid drift, we schedule execution against a fixed timeline.
// We define an interval and anchor the schedule to the time when the program starts.
TimeSpan interval = TimeSpan.FromDays(1);
DateTime next = DateTime.UtcNow;

while (true)
{
    // We call the API and get a JSON back. We store it in a string.
    string json = await requester.SendRequestAsync(url);

    // We deserialize the JSON package and store the metadata of the server content in a list.
    List<Metadata> metadata = deserializer.Deserialize(json);

    // Method for printing out the Metadata in console.
    //PrintInfo.PrintMetadata(metadata);

    // We save the metadata as a json file for easy storage using todays date.
    if (!fileManager.SaveToPath(json, $@"./Backup/{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}.json"))
    {
        // If we cant find the correct file path, throw an exception.
        throw new ArgumentException("Invalid save path.");
    }

    Console.WriteLine($"UTC: {DateTime.UtcNow} - Backup saved!");

    // We add the increment to next, and by doing so we define the next point in time when we want the code to execute.
    next = next.Add(interval);

    // We calculate how much time remains until the scheduled execution time.
    // By doing this, we're anchoring execution to a fixed timeline, and avoiding drift.
    TimeSpan delay = next - DateTime.UtcNow;

    // If execution takes longer than the interval, delay may become negative.
    // In that case, we skip waiting and immediately continue,
    // allowing the loop to catch up without throwing exceptions.
    if (delay > TimeSpan.Zero)
    {
        Console.WriteLine($"waiting for {delay} until {next}");
        await Task.Delay(delay);
    }
}

//Console.WriteLine(json);