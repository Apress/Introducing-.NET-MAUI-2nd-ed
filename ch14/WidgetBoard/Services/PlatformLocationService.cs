namespace WidgetBoard.Services;

public partial class PlatformLocationService : ILocationService
{
#if !ANDROID
    public Task<Location?> GetLocationAsync()
    {
        Location? location;
#if WINDOWS
        location = new Location(47.639722, -122.128333);
#elif MACCATALYST || IOS
        location = new Location(37.334722, -122.008889);
#else
        location = null;
#endif
        return Task.FromResult<Location?>(location);
    }
#endif
}