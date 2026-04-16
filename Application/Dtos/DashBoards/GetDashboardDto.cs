using Application.Dtos.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.DashBoards;

public class GetDashboardDto:CommonDto
{
    public int TotalProducts { get; set; }
    public int TotalCategories { get; set; }
    public int Revenue { get; set; }
    public int TotalOrders { get; set; }

}
