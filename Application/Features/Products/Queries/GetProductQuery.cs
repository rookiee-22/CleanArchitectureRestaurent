using Application.Dtos.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Application.Features.Products.Queries;

public class GetProductQuery:IRequest<Result<List<GetProductDto>>>
{

}
internal class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<List<GetProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetProductDto>>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Repository<Product>().Entities.Include(x=>x.Category).Where(c=>c.IsDeleted!=true).ToListAsync();
        var result= _mapper.Map<List<GetProductDto>>(products);
        return Result<List<GetProductDto>>.Success(result, "Products ");
    }
}