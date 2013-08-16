﻿using System.Data.Entity.ModelConfiguration;
using Trellendar.Domain.Trellendar;

namespace Trellendar.DataAccess.Local.ModelConfiguration.Configurations
{
    public class UserPreferencesConfiguration : EntityConfigurationBase<UserPreferences>
    {
        protected override void ConfigureEntity(EntityTypeConfiguration<UserPreferences> entity)
        {
            entity.HasKey(x => x.UserID);

            entity.HasRequired(x => x.User);

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
