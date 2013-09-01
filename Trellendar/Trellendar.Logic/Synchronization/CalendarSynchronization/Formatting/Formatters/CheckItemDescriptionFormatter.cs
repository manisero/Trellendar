using System.Collections.Generic;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Core.Extensions;
using System.Linq;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters
{
    public class CheckItemDescriptionFormatter : IDescriptionFormatter<CheckItem>
    {
        private const string CARD_SUMMARY_FORMAT = "Card: {0}";
        private const string CARD_URL_FORMAT = "Link: {0}";
        
        private readonly ISummaryFormatter<Card> _cardSummaryFormatter;

        public CheckItemDescriptionFormatter(ISummaryFormatter<Card> cardSummaryFormatter)
        {
            _cardSummaryFormatter = cardSummaryFormatter;
        }

        public string Format(CheckItem entity, UserPreferences userPreferences)
        {
            if (entity.CheckList == null || entity.CheckList.Card == null)
            {
                return null;
            }

            var cardSummary = _cardSummaryFormatter.Format(entity.CheckList.Card, userPreferences);
            var descriptionLines = new List<string>();

            if (cardSummary != null)
            {
                descriptionLines.Add(CARD_SUMMARY_FORMAT.FormatWith(cardSummary));
            }

            if (entity.CheckList.Card.Url != null)
            {
                descriptionLines.Add(CARD_URL_FORMAT.FormatWith(entity.CheckList.Card.Url));
            }

            return descriptionLines.Any() ? descriptionLines.JoinWith("\n") : null;
        }
    }
}