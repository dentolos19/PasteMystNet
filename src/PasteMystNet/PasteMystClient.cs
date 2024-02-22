namespace PasteMystNet;

public partial class PasteMystClient : IDisposable
{
    private readonly HttpClient _httpClient = new();

    public PasteMystClient(string? token = null)
    {
        _httpClient.BaseAddress = new Uri("https://paste.myst.rs/api/v2/");
        if (!string.IsNullOrEmpty(token))
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}