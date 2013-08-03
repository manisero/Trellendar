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

            entity.Property(x => x.ListShortcutBeginningMarker).HasMaxLength(10);
            entity.Property(x => x.ListShortcutEndMarker).HasMaxLength(10);
            entity.Property(x => x.CardEventNameTemplate).HasMaxLength(50);
        }
    }
}