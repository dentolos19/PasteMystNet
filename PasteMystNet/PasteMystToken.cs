namespace PasteMystNet;

public class PasteMystToken
{

    private readonly string _token;

    public PasteMystToken(string token)
    {
        _token = token;
    }

    public override string ToString()
    {
        return _token;
    }

}