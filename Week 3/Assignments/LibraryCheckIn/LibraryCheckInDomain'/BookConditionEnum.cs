using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCheckInDomain
{
    /// <summary>
    /// Represents the physical condition of a library book.
    ///  </summary>
    public enum BookCondition
    {
        New,
        Good,
        Worn,
        Damaged
    }
}
