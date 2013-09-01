using System;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic
{
    public class BoardCalendarContext
    {
        public BoardCalendarBond BoardCalendarBond { get; private set; }

        public BoardCalendarContext(BoardCalendarBond boardCalendarBond)
        {
            BoardCalendarBond = boardCalendarBond;
        }
    }

    public static class BoardCalendarContextExtensions
    {
        public static bool IsFilled(this BoardCalendarContext context)
        {
            return context != null && context.BoardCalendarBond != null;
        }

        public static bool HasBondSettings(this BoardCalendarContext context)
        {
            return context.IsFilled() && context.BoardCalendarBond.Settings != null;
        }

        public static BoardCalendarBondSettings GetSettings(this BoardCalendarContext context)
        {
            return context.HasBondSettings() ? context.BoardCalendarBond.Settings : null;
        }

        public static string GetCardEventNameTemplateSetting(this BoardCalendarContext context)
        {
            return context.HasBondSettings()
                       ? context.BoardCalendarBond.Settings.CardEventNameTemplate
                       : null;
        }

        public static string GetCheckListEventNameTemplateSetting(this BoardCalendarContext context)
        {
            return context.HasBondSettings()
                       ? context.BoardCalendarBond.Settings.CheckListEventNameTemplate
                       : null;
        }

        public static string GetCheckListEventDoneSuffixSetting(this BoardCalendarContext context)
        {
            return context.HasBondSettings()
                       ? context.BoardCalendarBond.Settings.CheckListEventDoneSuffix
                       : null;
        }

        public static TimeSpan? GetWholeDayEventDueTimeSetting(this BoardCalendarContext context)
        {
            return context.HasBondSettings()
                       ? context.BoardCalendarBond.Settings.WholeDayEventDueTime
                       : null;
        }
    }
}
