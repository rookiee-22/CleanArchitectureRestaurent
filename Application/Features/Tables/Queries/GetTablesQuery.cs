using Application.Dtos.Commons;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Tables;
using MediatR;
using Shared;
using Application.Dtos.Commons;
using Application.Dtos.Tables;

namespace Application.Features.Tables.Queries;

public class GetTablesQuery : IRequest<Result<List<GetTableDto>>>
{

}
internal class GetTablesQueryHandler : IRequestHandler<GetTablesQuery, Result<List<GetTableDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTablesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetTableDto>>> Handle(GetTablesQuery request, CancellationToken cancellationToken)
    {
        var tables = await _unitOfWork.Repository<Table>().GetAllAsync();
        var result = _mapper.Map<List<GetTableDto>>(tables);
        return Result<List<GetTableDto>>.Success(result, "Tables");
    }
}
