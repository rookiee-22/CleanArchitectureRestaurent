using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tables;

public class Table :BaseAuditableEntity
{
    public int TableNumber { get; set; }
}
