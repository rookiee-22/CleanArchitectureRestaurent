using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Domain.Entities.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Profiles;

public class GetProfileDto:CommonDto,IMapFrom<Profile>
{
    public string ImageUrl { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public int UserId { get; set; }
}
