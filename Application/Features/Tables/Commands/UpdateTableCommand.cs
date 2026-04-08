using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Tables;
using MediatR;
using Shared;

namespace Application.Features.Tables.Commands;

public class UpdateTableCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public CreateTableCommand CreateTable { get; set; }
    public UpdateTableCommand(int id, CreateTableCommand createTable)
    {
        Id = id;
        CreateTable = createTable;
    }
}
internal class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTableCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        var table = await _unitOfWork.Repository<Table>().GetByIdAsync(request.Id);
        if (table == null)
        {
            return Result<string>.BadRequest("Table not found");
        }
        _mapper.Map(request.CreateTable, table);
        await _unitOfWork.Repository<Table>().UpdateAsync(table);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Table updated Succesfully");
    }
}
