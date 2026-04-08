using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Commons;

public class CommonDto
{
    public int Id { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; }
}
