namespace PasteMystNet;

public class PasteMystException(
    string message,
    Exception inner,
    Uri requestUri,
    string? requestContent
) : Exception(message, inner)
{
    public Uri RequestUri { get; } = requestUri;
    public string? RequestContent { get; } = requestContent;
}