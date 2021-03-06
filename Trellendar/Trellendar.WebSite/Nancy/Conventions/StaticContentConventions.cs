﻿using System;
using Nancy.Conventions;

namespace Trellendar.WebSite.Nancy.Conventions
{
    public class StaticContentConventions : IConvention
    {
        public void Initialise(NancyConventions conventions)
        {
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Css", "Css", "css"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Scripts", "Scripts", "js"));
        }

        public Tuple<bool, string> Validate(NancyConventions conventions)
        {
            return new Tuple<bool, string>(true, null);
        }
    }
}