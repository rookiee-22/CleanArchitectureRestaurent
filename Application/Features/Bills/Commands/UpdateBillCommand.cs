using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Commons.Enums.BIlls;
using Domain.Entities.Bills;
using MediatR;
using Shared;

namespace Application.Features.Bills.Commands;

public class UpdateBillCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
   public BillStatus Status { get; set; }
    public UpdateBillCommand(int id, BillStatus status)
    {
        Id = id;
       Status = status;
    }
}
internal class UpdateBillCommandHandler : IRequestHandler<UpdateBillCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBillCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateBillCommand request, CancellationToken cancellationToken)
    {
        //if (request.CreateBill.TableId != 0)
        //{
        //    var table = await _unitOfWork.Repository<Domain.Entities.Tables.Table>().GetByIdAsync(request.CreateBill.TableId);
        //    if (table == null)
        //    {
        //        return Result<string>.BadRequest("Table Id is not exist.");
        //    }
        //}

        var bill = await _unitOfWork.Repository<Bill>().GetByIdAsync(request.Id);
        if (bill == null)
        {
            return Result<string>.BadRequest("Bill Not Found");
        }
        bill.Status = request.Status;
        //_mapper.Map(request.CreateBill, bill);
        await _unitOfWork.Repository<Bill>().UpdateAsync(bill);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Bill Updated Succesfully");
    }
}
