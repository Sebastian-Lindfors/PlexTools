
namespace PlexTools.Core;

// Interface for future API fuckery.
// Interface is best practice here, because Core-functionality should not contain any dependencies.
public interface IApiRequester
{
    Task<string> SendRequestAsync(string url);
}
