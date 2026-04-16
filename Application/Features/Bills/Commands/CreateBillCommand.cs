using Application.Commons.Mappings.Commons;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.BillItems;
using Domain.Entities.Bills;
using Domain.Entities.Tables;
using MediatR;
using Shared;

namespace Application.Features.Bills.Commands;

public class CreateBillCommand : IRequest<Result<int>>, ICreateMapFrom<Bill>
{
    public int TableId { get; set; }
    public string Note { get; set; }
    


}
internal class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBillCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        var table = await _unitOfWork.Repository<Table>().GetByIdAsync(request.TableId);
        
        if (table == null)
        {
            return Result<int>.BadRequest("Table id not exist");
        }

        var bill = _mapper.Map<Bill>(request);
        await _unitOfWork.Repository<Bill>().CreateAsync(bill);
        await _unitOfWork.Save(cancellationToken);
        return Result<int>.Success(bill.Id, "Bill Created Successfully");
    }
}
