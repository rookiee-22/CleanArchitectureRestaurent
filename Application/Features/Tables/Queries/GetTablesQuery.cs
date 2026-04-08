using Application.Dtos.Commons;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Tables;
using MediatR;
using Shared;
using Application.Dtos.Commons;

namespace Application.Features.Tables.Queries;

public class GetTablesQuery : IRequest<Result<List<CommonDto>>>
{
}
internal class GetTablesQueryHandler : IRequestHandler<GetTablesQuery, Result<List<CommonDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTablesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<CommonDto>>> Handle(GetTablesQuery request, CancellationToken cancellationToken)
    {
        var tables = await _unitOfWork.Repository<Table>().GetAllAsync();
        var result = _mapper.Map<List<CommonDto>>(tables);
        return Result<List<CommonDto>>.Success(result, "Tables");
    }
}
