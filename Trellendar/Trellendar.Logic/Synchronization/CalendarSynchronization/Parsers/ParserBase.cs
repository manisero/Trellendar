using System;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Parsers
{
    public abstract class ParserBase<TOutput> : IParser<TOutput>
    {
        public TOutput Parse(string text, UserPreferences userPreferences)
        {
            if (text == null)
            {
                return default(TOutput);
            }

            var outputTextMarkers = GetOutputTextMarkers(userPreferences);

            if (outputTextMarkers == null || outputTextMarkers.Item1 == null || outputTextMarkers.Item2 == null)
            {
                return GetDefaultOutput(text);
            }

            var searchStartIndex = 0;

            while (true)
            {
                var beginningIndex = text.IndexOf(outputTextMarkers.Item1, searchStartIndex, StringComparison.Ordinal);

                if (beginningIndex < 0)
                {
                    return GetDefaultOutput(text);
                }

                var endIndex = text.IndexOf(outputTextMarkers.Item2, beginningIndex, StringComparison.Ordinal);

                if (endIndex < 0)
                {
                    return GetDefaultOutput(text);
                }

                searchStartIndex = beginningIndex + 1;

                var outputText = text.Substring(beginningIndex + outputTextMarkers.Item1.Length,
                                                endIndex - beginningIndex - outputTextMarkers.Item1.Length);

                TOutput output;
                
                if (TryGetOutput(outputText, out output))
                {
                    return output;
                }
            }
        }

        protected virtual TOutput GetDefaultOutput(string text)
        {
            return default(TOutput);
        }

        protected abstract Tuple<string, string> GetOutputTextMarkers(UserPreferences userPreferences);

        protected abstract bool TryGetOutput(string outputText, out TOutput output);
    }
}
