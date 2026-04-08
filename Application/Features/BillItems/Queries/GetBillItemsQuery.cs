using Application.Dtos.BillItems;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.BillItems;
using MediatR;
using Shared;

namespace Application.Features.BillItems.Queries;

public class GetBillItemsQuery : IRequest<Result<List<GetBillItemDto>>>
{
}
internal class GetBillItemsQueryHandler : IRequestHandler<GetBillItemsQuery, Result<List<GetBillItemDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBillItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetBillItemDto>>> Handle(GetBillItemsQuery request, CancellationToken cancellationToken)
    {
        var billItems = await _unitOfWork.Repository<BillItem>().GetAllAsync();
        var result = _mapper.Map<List<GetBillItemDto>>(billItems);
        return Result<List<GetBillItemDto>>.Success(result, "BillItems");
    }
}
