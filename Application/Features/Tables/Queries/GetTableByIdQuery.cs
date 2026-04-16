using Application.Dtos.Commons;
using Application.Dtos.Tables;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Tables;
using MediatR;
using Shared;

namespace Application.Features.Tables.Queries;

public class GetTableByIdQuery : IRequest<Result<GetTableDto>>
{
    public int Id { get; set; }
    public GetTableByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, Result<GetTableDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTableByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetTableDto>> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
    {
        var table = await _unitOfWork.Repository<Table>().GetByIdAsync(request.Id);
        var result = _mapper.Map<GetTableDto>(table);
        return Result<GetTableDto>.Success(result, "Table");
    }
}
