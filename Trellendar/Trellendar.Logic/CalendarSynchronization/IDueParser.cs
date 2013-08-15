﻿using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.CalendarSynchronization
{
    public interface IParser<TOutput>
    {
        TOutput Parse(string text, UserPreferences userPreferences);
    }
}
