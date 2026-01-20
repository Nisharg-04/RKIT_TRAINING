using ExpenseTracker.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.BAL
{
    public interface IAuthService
    {
        void Register(RegisterUserDTO request);
        string Login(LoginUserDTO request);
    }
}