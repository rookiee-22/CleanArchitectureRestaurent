using Application.Dtos.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Products.Queries;

public class GetProductByIdQuery:IRequest<Result<GetProductDto>>
{
    public int Id { get; set; }
    public GetProductByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery,Result<GetProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);
        var result= _mapper.Map<GetProductDto>(product);
        return Result<GetProductDto>.Success(result, "Product");
    }
}