using Application.Dtos.Carts;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Carts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Carts.Queries;

public class GetCartsQuery : IRequest<Result<List<GetCartDto>>>
{
}
internal class GetCartsQueryHandler : IRequestHandler<GetCartsQuery, Result<List<GetCartDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCartsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetCartDto>>> Handle(GetCartsQuery request, CancellationToken cancellationToken)
    {

        //var carts = await _unitOfWork.Repository<Cart>().Entities.Include(x=>x.Items).ToListAsync();
        //    var carts = await _unitOfWork.Repository<Cart>()
        //.Entities
        //    .Include(c => c.Items).ThenInclude(i => i.Product).Where(x=>x.IsDeleted!=true).ToListAsync();
        var carts = await _unitOfWork.Repository<Cart>()
        .Entities
        .Include(c => c.Items.Where(i => i.IsDeleted != true))
            .ThenInclude(i => i.Product)
        .Where(x => x.IsDeleted != true)
        .ToListAsync();
        //.ThenInclude(i => i.Product)


        //var carts = await _unitOfWork.Repository<Cart>().GetAllAsync();
        var result = _mapper.Map<List<GetCartDto>>(carts);
        return Result<List<GetCartDto>>.Success(result, "Carts");
    }
}
