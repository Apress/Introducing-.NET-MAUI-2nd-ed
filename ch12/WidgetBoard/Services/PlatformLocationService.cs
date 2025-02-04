namespace WidgetBoard.Services;

public partial class PlatformLocationService : ILocationService
{
#if IOS || MACCATALYST || WINDOWS
    public Task<Location?> GetLocationAsync()
    {
        Location? location;
#if WINDOWS
        location = new Location(47.639722, -122.128333);
#else
        location = new Location(37.334722, -122.008889);
#endif
        return Task.FromResult(location);
    }
#endif
}