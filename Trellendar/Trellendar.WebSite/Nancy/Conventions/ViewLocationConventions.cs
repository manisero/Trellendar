using System;
using Nancy.Conventions;
using Trellendar.Core.Extensions;

namespace Trellendar.WebSite.Nancy.Conventions
{
    public class ViewLocationConventions : IConvention
    {
        public void Initialise(NancyConventions conventions)
        {
            conventions.ViewLocationConventions.Insert(0, (viewName, model, context) => "Modules/{0}/Views/{1}".FormatWith(context.ModuleName, viewName));
            conventions.ViewLocationConventions.Insert(1, (viewName, model, context) => "Modules/{0}/{1}".FormatWith(context.ModuleName, viewName));
        }

        public Tuple<bool, string> Validate(NancyConventions conventions)
        {
            return new Tuple<bool, string>(true, null);
        }
    }
}