﻿using System.Data.Entity.ModelConfiguration;
using Trellendar.Domain.Trellendar;

namespace Trellendar.DataAccess.Local.ModelConfiguration.Configurations
{
    public class BoardCalendarBondSettingsConfiguration : EntityConfigurationBase<BoardCalendarBondSettings>
    {
        protected override void ConfigureEntity(EntityTypeConfiguration<BoardCalendarBondSettings> entity)
        {
            entity.HasKey(x => new { x.UserID, x.BoardID, x.CalendarID });

            entity.HasRequired(x => x.BoardCalendarBond);

            entity.Property(x => x.TrelloItemShortcutBeginningMarker).HasMaxLength(10);
            entity.Property(x => x.TrelloItemShortcutEndMarker).HasMaxLength(10);
            entity.Property(x => x.CardEventNameTemplate).HasMaxLength(50);
            entity.Property(x => x.CheckListEventNameTemplate).HasMaxLength(50);
            entity.Property(x => x.CheckListEventDoneSuffix).HasMaxLength(50);
            entity.Property(x => x.DueTextBeginningMarker).HasMaxLength(10);
            entity.Property(x => x.DueTextEndMarker).HasMaxLength(10);
            entity.Property(x => x.LocationTextBeginningMarker).HasMaxLength(10);
            entity.Property(x => x.LocationTextEndMarker).HasMaxLength(10);
        }
    }
}
