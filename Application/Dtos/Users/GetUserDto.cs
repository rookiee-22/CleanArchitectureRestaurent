using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Users;

public class GetUserDto:CommonDto,IMapFrom<User>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public long MobileNo { get; set; }
    public int Role { get; set; }


}
