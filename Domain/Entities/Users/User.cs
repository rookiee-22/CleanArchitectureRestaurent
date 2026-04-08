using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Users;

public class User:BaseAuditableEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public long MobileNo { get; set; }
    public string Password { get; set; }

}
