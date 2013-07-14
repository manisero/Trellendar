namespace Trellendar.DataAccess.Trello
{
    public interface ITrelloAccessTokenProvider
    {
        bool CanProvideTrelloAccessToken { get; }

        string GetTrelloAccessToken();
    }
}
