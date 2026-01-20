using ExpenseTracker.Common;
using ExpenseTracker.DAL;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTOs;
using ExpenseTracker.Models.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.BAL
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly NLogLogger _logger;

        public AuthService(IUserRepository repo, NLogLogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public void Register(RegisterUserDTO request)
        {
            if (_repo.GetByUsername(request.Username) != null)
                throw new Exception("User already exists");

            var user = new usrt01
            {
                usrf02 = request.Username,
                usrf03 = PasswordHasher.Hash(request.Password),
                 usrf04= DateTime.UtcNow
            };

            _repo.Insert(user);
            _logger.Info($"User registered: {request.Username}");
        }

        public string Login(LoginUserDTO request)
        {
            var user = _repo.GetByUsername(request.Username);
            if (user == null)
                throw new Exception("Invalid credentials");

            var hash = PasswordHasher.Hash(request.Password);
            if (user.usrf03 != hash)
                throw new Exception("Invalid credentials");

            _logger.Info($"User logged in: {request.Username}");

            return JwtHelper.GenerateToken(user.usrf01, user.usrf02);
        }
    }

}