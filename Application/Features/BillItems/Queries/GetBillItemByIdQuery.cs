using Application.Dtos.BillItems;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.BillItems;
using MediatR;
using Shared;

namespace Application.Features.BillItems.Queries;

public class GetBillItemByIdQuery : IRequest<Result<GetBillItemDto>>
{
    public int Id { get; set; }
    public GetBillItemByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetBillItemByIdQueryHandler : IRequestHandler<GetBillItemByIdQuery, Result<GetBillItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBillItemByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetBillItemDto>> Handle(GetBillItemByIdQuery request, CancellationToken cancellationToken)
    {
        var billItem = await _unitOfWork.Repository<BillItem>().GetByIdAsync(request.Id);
        var result = _mapper.Map<GetBillItemDto>(billItem);
        return Result<GetBillItemDto>.Success(result, "BillItem");
    }
}
