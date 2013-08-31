using System;
using System.Data.Entity;
using System.Linq;
using Trellendar.Core.Extensions;

namespace Trellendar.DataAccess.Local.ModelConfiguration
{
    public class ModelConfigurator
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            var types = GetType().Assembly.DefinedTypes.Where(x => x.Namespace == "{0}.Configurations".FormatWith(GetType().Namespace));

            var configurations = types.Select(x => x.GetConstructor(new Type[0]).Invoke(new object[0]) as IEntityConfiguration);

            configurations.ForEach(x => x.Configure(modelBuilder));
        }
    }
}
