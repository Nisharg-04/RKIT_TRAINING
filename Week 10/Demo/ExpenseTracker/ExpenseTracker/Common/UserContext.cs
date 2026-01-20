using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace ExpenseTracker.Common
{
    public static class UserContext
    {
        public static int GetUserId(ApiController controller)
        {
            var identity = controller.User.Identity as ClaimsIdentity;
            var claim = identity?.FindFirst("userId");

            return int.Parse(claim.Value);
        }
    }
}