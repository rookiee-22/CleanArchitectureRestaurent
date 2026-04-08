using Application.Commons.Mappings.Commons;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Carts;
using MediatR;
using Shared;

namespace Application.Features.Carts.Commands;

public class CreateCartCommand : IRequest<Result<int>>, ICreateMapFrom<Cart>
{
    public int TableId { get; set; }
    public string Note { get; set; }
}
internal class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var table = await _unitOfWork.Repository<Domain.Entities.Tables.Table>().GetByIdAsync(request.TableId);
        if (table == null)
        {
            return Result<int>.BadRequest("Table id not exist");
        }
        var cart = _mapper.Map<Cart>(request);
        await _unitOfWork.Repository<Cart>().CreateAsync(cart);
        await _unitOfWork.Save(cancellationToken);
        return Result<int>.Success(cart.Id, "Cart Created Successfully");
    }
}
