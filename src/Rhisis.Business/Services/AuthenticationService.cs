﻿using Rhisis.Core.Common;
using Rhisis.Core.DependencyInjection;
using Rhisis.Core.Services;
using Rhisis.Database;
using Rhisis.Database.Entities;
using System;

namespace Rhisis.Business.Services
{
    [Injectable]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDatabase _database;

        /// <summary>
        /// Creates a new <see cref="AuthenticationService"/> instance.
        /// </summary>
        /// <param name="database"></param>
        public AuthenticationService(IDatabase database)
        {
            this._database = database;
        }

        /// <inheritdoc />
        public AuthenticationResult Authenticate(string username, string password)
        {
            DbUser _dbUser = this._database.Users.Get(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (_dbUser == null)
            {
                return AuthenticationResult.BadUsername;
            }

            if (_dbUser.IsDeleted)
            {
                return AuthenticationResult.AccountDeleted;
            }

            if (!_dbUser.Password.Equals(password, StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticationResult.BadPassword;
            }

            return AuthenticationResult.Success;
        }
    }
}
