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

        public static string GetPrefferedCardEventNameTemplate(this BoardCalendarContext context)
        {
            return context.HasBondSettings()
                       ? context.BoardCalendarBond.Settings.CardEventNameTemplate
                       : null;
        }

        public static string GetPrefferedCheckListEventNameTemplate(this BoardCalendarContext context)
        {
            return context.HasBondSettings()
                       ? context.BoardCalendarBond.Settings.CheckListEventNameTemplate
                       : null;
        }

        public static string GetPrefferedCheckListEventDoneSuffix(this BoardCalendarContext context)
        {
            return context.HasBondSettings()
                       ? context.BoardCalendarBond.Settings.CheckListEventDoneSuffix
                       : null;
        }

        public static TimeSpan? GetPrefferedWholeDayEventDueTime(this BoardCalendarContext context)
        {
            return context.HasBondSettings()
                       ? context.BoardCalendarBond.Settings.WholeDayEventDueTime
                       : null;
        }
    }
}
