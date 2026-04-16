using Application.Dtos.DashBoards;
using Application.Interfaces.Repositories;
using Domain.Entities.Bills;
using Domain.Entities.Categories;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Dashboards;

public class GetDashboardQuery : IRequest<Result<GetDashboardDto>>
{

    
}
internal class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, Result<GetDashboardDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetDashboardQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<GetDashboardDto>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Repository<Product>().GetAllAsync();
        var productCount = products.Count();
        var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
        var categoryCount = categories.Count();
        var orders = await _unitOfWork.Repository<Order>().GetAllAsync();
        var orderCount = orders.Count();
        //var bills = await _unitOfWork.Repository<Bill>().GetAllAsync();
        var bills = await _unitOfWork.Repository<Bill>()
       .Entities
       .Include(b => b.BillItems.Where(i => i.IsDeleted != true))
       .Where(b => b.IsDeleted != true)
       .ToListAsync();
        //var revenue = bills.Sum(b => b.TotalAmount);
        var revenue = bills.Sum(b => b.BillItems.Sum(i => i.Price * i.Quantity));
        var dashboard = new GetDashboardDto
        {
            TotalProducts = productCount,
            TotalCategories = categoryCount,
            Revenue = (int)revenue,
            TotalOrders = orderCount
        };
        return Result<GetDashboardDto>.Success(dashboard, "Dashboard");
    }
}
