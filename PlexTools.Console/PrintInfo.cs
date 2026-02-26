
using PlexTools.API;

namespace PlexTools.Console;

class PrintInfo
{
    public static void PrintMetadata(List<Metadata> metadata)
    {


        foreach (var data in metadata)
        {
            System.Console.WriteLine("---------------------------------------------------------------");
            System.Console.WriteLine($"Title:      {data.title}");
            System.Console.WriteLine($"Studio:     {data.studio}");
            System.Console.WriteLine($"Age Rating: {data.contentRating}");
            System.Console.Write($"\nSummary:\n{data.summary}\n\n");
        }
    }
}