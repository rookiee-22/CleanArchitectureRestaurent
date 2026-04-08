using Application.Commons.Mappings.Commons;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Tables;
using MediatR;
using Shared;

namespace Application.Features.Tables.Commands;

public class CreateTableCommand : IRequest<Result<int>>, ICreateMapFrom<Table>
{
    public int TableNumber { get; set; }
}
internal class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTableCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        var table = _mapper.Map<Table>(request);
        await _unitOfWork.Repository<Table>().CreateAsync(table);
        await _unitOfWork.Save(cancellationToken);
        return Result<int>.Success(table.Id, "Table Created Successfully");
    }
}
