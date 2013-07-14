namespace Trellendar.DataAccess
{
    public interface IAccessTokenProvider
    {
        bool CanProvideAccessToken { get; }

        string GetAccessToken();
    }
}
