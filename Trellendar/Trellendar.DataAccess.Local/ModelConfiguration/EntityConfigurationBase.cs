﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Trellendar.DataAccess.Local.ModelConfiguration
{
    public abstract class EntityConfigurationBase<TEntity> : IEntityConfiguration
        where TEntity : class
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            ConfigureEntity(modelBuilder.Entity<TEntity>());
        }

        protected abstract void ConfigureEntity(EntityTypeConfiguration<TEntity> entity);
    }
}
