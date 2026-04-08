using Domain.Commons;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Profiles;

public class Profile :BaseAuditableEntity
{
    public string ImageUrl { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
}
