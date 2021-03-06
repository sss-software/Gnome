﻿using Gnome.Core.DataAccess;
using Gnome.Core.Model.Database;
using Gnome.Core.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace Gnome.Core.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IUserSecurityRepository securityRepository;
        private readonly IUserSecurityService securityService;

        public UserService(
            IUserRepository repository,
            IUserSecurityRepository securityRepository,
            IUserSecurityService securityService)
        {
            this.repository = repository;
            this.securityRepository = securityRepository;
            this.securityService = securityService;
        }

        public async Task<bool> CheckEmailAvailability(string email)
        {
            return await repository.CheckEmailAvailability(email);
        }

        public void CreateNew(string email, string password, Guid userId)
        {
            var salt = securityService.GetSalt();
            var pwd = securityService.CreatePassword(password, salt);
            var result = securityRepository.CreateNew(email, pwd, salt, userId);

            if (result == null)
                throw new InvalidOperationException("user was not registered");
        }

        public User Verify(string email, string password)
        {
            var user = securityRepository.GetBy(email);
            if (user == null)
                return null;

            if (!securityService.Verify(password, user.Password, user.Salt))
                return null;

            return new User()
            {
                Id = user.Id,
                Email = user.Email
            };
        }
    }
}