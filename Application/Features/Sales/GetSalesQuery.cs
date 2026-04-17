using Application.Dtos.Sales;
using Application.Interfaces.Repositories;
using Domain.Entities.Bills;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Sales;

public class GetSalesQuery:IRequest<Result<GetSalesDto>>
{

}

internal class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, Result<GetSalesDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSalesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetSalesDto>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
       

        var today = DateTime.Today;

    var weekStart = today.AddDays(-(int)today.DayOfWeek);
        if (today.DayOfWeek == DayOfWeek.Sunday)
            weekStart = today;

        var monthStart = new DateTime(today.Year, today.Month, 1);

    var bills = await _unitOfWork.Repository<Bill>()
        .Entities
        .Include(b => b.BillItems.Where(x => x.IsDeleted != true))
        .Where(b => b.IsDeleted != true)
        .ToListAsync(cancellationToken);

    var todaySales = bills
        .Where(x => x.CreatedDate.HasValue &&
                    x.CreatedDate.Value.Date == today)
        .Sum(x => x.BillItems.Sum(i => i.Price * i.Quantity));

    var weekSales = bills
        .Where(x => x.CreatedDate.HasValue &&
                    x.CreatedDate.Value.Date >= weekStart &&
                    x.CreatedDate.Value.Date <= today)
        .Sum(x => x.BillItems.Sum(i => i.Price * i.Quantity));

    var monthSales = bills
        .Where(x => x.CreatedDate.HasValue &&
                    x.CreatedDate.Value.Date >= monthStart &&
                    x.CreatedDate.Value.Date <= today)
        .Sum(x => x.BillItems.Sum(i => i.Price * i.Quantity));

    var totalOrders = bills.Count();

    

    var sales = new GetSalesDto
    {
        TodaySales = todaySales,
        ThisWeekSales = weekSales,
        ThisMonthSales = monthSales,
        TotalOrders = totalOrders,
       
    };

        return Result<GetSalesDto>.Success(sales, "Sales Dashboard");
    }

 
}