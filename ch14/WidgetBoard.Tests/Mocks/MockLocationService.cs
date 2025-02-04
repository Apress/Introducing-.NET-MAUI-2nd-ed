using WidgetBoard.Services;

namespace WidgetBoard.Tests.Mocks;

public class MockLocationService : ILocationService
{
    private readonly Location? location;
    private readonly TimeSpan delay;

    private MockLocationService(Location? mockLocation, TimeSpan delay)
    {
        location = mockLocation;
        this.delay = delay;
    }

    public static ILocationService ThatReturns(Location? location, TimeSpan after) =>
        new MockLocationService(location, after);

    public static ILocationService ThatReturnsNoLocation(TimeSpan after) =>
        new MockLocationService(null, after);

    public async Task<Location?> GetLocationAsync()
    {
        await Task.Delay(this.delay);
        return this.location;
    }
}