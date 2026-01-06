using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExceptionDemo.Exceptions
{
    public class BusinessException: Exception
    {
        public string ErrorCode { get; }

        public BusinessException(string message, string errorCode = "BUSINESS_ERROR")
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}