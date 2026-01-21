using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace InterfaceBasedFilters.Filters
{
    public class OverideFilter : Attribute,IOverrideFilter
    {
        public Type FiltersToOverride => typeof(IActionFilter);

        public bool AllowMultiple => false;
    }
}