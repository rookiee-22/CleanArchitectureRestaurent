using Domain.Commons.Enums.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories;

public interface IGenerateToken
{
    public string GenerateToken(string email,Role role);
}
