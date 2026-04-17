using Application.Dtos.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Sales;

public class GetSalesDto:CommonDto
{
    public decimal TodaySales { get; set; }
    public decimal ThisWeekSales { get; set; }
    public decimal ThisMonthSales { get; set; }
    public int TotalOrders { get; set; }
}
