namespace Trellendar.DataAccess.Remote
{
    public interface IAccessTokenProvider
    {
        bool CanProvideAccessToken { get; }

        string GetAccessToken();
    }
}
