namespace WidgetBoard.Tests.Mocks;

public class MockSecureStorage : ISecureStorage
{
    private readonly Dictionary<string, string?> values = new();
    
    private MockSecureStorage(string key, string value)
    {
        values.Add(key, value);
    }

    public static MockSecureStorage ThatContains(string key, string value) =>
        new MockSecureStorage(key, value);
    
    public Task<string?> GetAsync(string key)
    {
        return Task.FromResult(values[key]);
    }

    public Task SetAsync(string key, string value)
    {
        values[key] = value;
        return Task.CompletedTask;
    }

    public bool Remove(string key)
    {
        if (values.ContainsKey(key))
        {
            values.Remove(key);
            return true;
        }
        
        return false;
    }

    public void RemoveAll()
    {
        values.Clear();
    }
}