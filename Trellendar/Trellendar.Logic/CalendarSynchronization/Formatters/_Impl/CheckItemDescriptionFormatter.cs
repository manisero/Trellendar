using System.Collections.Generic;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Core.Extensions;
using System.Linq;

namespace Trellendar.Logic.CalendarSynchronization.Formatters._Impl
{
    public class CheckItemDescriptionFormatter : ICheckItemDescriptionFormatter
    {
        private const string CARD_SUMMARY_FORMAT = "Card: {0}";
        private const string CARD_URL_FORMAT = "Link: {0}";
        
        private readonly ICardSummaryFormatter _cardSummaryFormatter;

        public CheckItemDescriptionFormatter(ICardSummaryFormatter cardSummaryFormatter)
        {
            _cardSummaryFormatter = cardSummaryFormatter;
        }

        public string Format(CheckItem checkItem, UserPreferences userPreferences)
        {
            if (checkItem.CheckList == null || checkItem.CheckList.Card == null)
            {
                return null;
            }

            var cardSummary = _cardSummaryFormatter.Format(checkItem.CheckList.Card, userPreferences);
            var descriptionLines = new List<string>();

            if (cardSummary != null)
            {
                descriptionLines.Add(CARD_SUMMARY_FORMAT.FormatWith(cardSummary));
            }

            if (checkItem.CheckList.Card.Url != null)
            {
                descriptionLines.Add(CARD_URL_FORMAT.FormatWith(checkItem.CheckList.Card.Url));
            }

            return descriptionLines.Any() ? descriptionLines.JoinWith("\n") : null;
        }
    }
}