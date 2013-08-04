using System;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public abstract class SingleBoardItemProcessorBase
    {
        protected string FormatParentName(string parentName, Tuple<string, string> shortcutMarkers)
        {
            if (shortcutMarkers != null &&
                shortcutMarkers.Item1 != null && shortcutMarkers.Item2 != null &&
                parentName.Contains(shortcutMarkers.Item1) && parentName.Contains(shortcutMarkers.Item2))
            {
                var beginningIndex = parentName.LastIndexOf(shortcutMarkers.Item1);
                var endIndex = parentName.LastIndexOf(shortcutMarkers.Item2);

                if (endIndex > beginningIndex)
                {
                    return parentName.Substring(beginningIndex + shortcutMarkers.Item1.Length,
                                              endIndex - beginningIndex - shortcutMarkers.Item1.Length);
                }
            }

            return parentName;
        }
    }
}
