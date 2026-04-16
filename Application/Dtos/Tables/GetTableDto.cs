using Application.Commons.Mappings.Commons;
using Application.Dtos.Commons;
using Domain.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Tables;

public class GetTableDto:CommonDto,IMapFrom<Table>
{
    public int TableNumber { get; set; }
}
