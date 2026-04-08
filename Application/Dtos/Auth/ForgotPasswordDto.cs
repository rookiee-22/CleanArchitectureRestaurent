using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Auth;

public class ForgotPasswordDto
{
    public string Email { get; set; }
}
