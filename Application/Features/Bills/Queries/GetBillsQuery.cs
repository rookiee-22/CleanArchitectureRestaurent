using Application.Dtos.Bills;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Bills;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Bills.Queries;

public class GetBillsQuery : IRequest<Result<List<GetBillDto>>>
{
}
internal class GetBillsQueryHandler : IRequestHandler<GetBillsQuery, Result<List<GetBillDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBillsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetBillDto>>> Handle(GetBillsQuery request, CancellationToken cancellationToken)
    {
        var bills = await _unitOfWork.Repository<Bill>().Entities.Include(c => c.BillItems.Where(i => i.IsDeleted != true)).ThenInclude(i => i.Product).Where(x => x.IsDeleted != true).ToListAsync();
        //var bills = await _unitOfWork.Repository<Bill>().GetAllAsync();
        var result = _mapper.Map<List<GetBillDto>>(bills);
        foreach (var bill in result)
        {
            bill.TotalAmount = bill.BillItems.Sum(item => item.Total);
        }
        return Result<List<GetBillDto>>.Success(result, "Bills");
    }
}
