using System;
using System.Net.Http;

namespace Trellendar
{
    public class TrelloAPI
    {
        public void Test()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("https://api.trello.com/1/") };

            var response = httpClient.GetAsync(@"https://api.trello.com/1/board/4d5ea62fd76aa1136000000c").Result.Content.ReadAsStringAsync().Result;
        }
    }
}
