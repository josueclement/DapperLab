using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt.Common;

namespace DapperLab.Domain;

public interface IUserRepository
{
    Task<Result<IEnumerable<User>>> GetUsersAsync();
    Task<Result<User>> GetUserAsync(Ulid id);
}