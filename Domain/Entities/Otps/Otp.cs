using Domain.Commons;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Otps;

public class Otp : BaseAuditableEntity
{
    public int Code { get; set; }
    public DateTime CreateDate { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }

}
