using Application.Dtos.Bills;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Bills;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Bills.Queries;

public class GetBillByIdQuery : IRequest<Result<GetBillDto>>
{
    public int Id { get; set; }
    public GetBillByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetBillByIdQueryHandler : IRequestHandler<GetBillByIdQuery, Result<GetBillDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBillByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetBillDto>> Handle(GetBillByIdQuery request, CancellationToken cancellationToken)
    {

        //var bill = await _unitOfWork.Repository<Bill>().GetByIdAsync(request.Id);
        var bill = await _unitOfWork.Repository<Bill>()
       .Entities
       .Include(c => c.BillItems.Where(i => i.IsDeleted != true))
       .ThenInclude(i => i.Product)
       .Where(x => x.IsDeleted != true)
       .FirstOrDefaultAsync(x => x.Id == request.Id);
        var result = _mapper.Map<GetBillDto>(bill);
        result.TotalAmount = result.BillItems.Sum(item => item.Price * item.Quantity);
        return Result<GetBillDto>.Success(result, "Bill");
    }
}
