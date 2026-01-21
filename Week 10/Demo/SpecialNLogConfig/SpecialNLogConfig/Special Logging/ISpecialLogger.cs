using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpecialNLogConfig.Special_Logging
{
    public interface ISpecialLogger
    {
        void Audit(string message);
    }

}