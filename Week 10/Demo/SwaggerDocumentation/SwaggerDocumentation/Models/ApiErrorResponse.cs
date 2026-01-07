using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwaggerDocumentation.Models
{
    /// <summary>
    /// Standard API error response
    /// </summary>
    public class ApiErrorResponse
    {
        /// <summary>Error message</summary>
        public string Message { get; set; }

        /// <summary>Error code</summary>
        public int Code { get; set; }
    }

}