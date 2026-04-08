using Application.Dtos.Categories;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Categories;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Categories.Queries;

public class GetCategoryByIdQuery:IRequest<Result<GetCategoryDto>>
{
    public int Id { get; set; }
    public GetCategoryByIdQuery(int id)
    {
        Id=id;
    }
}

internal class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



  public async  Task<Result<GetCategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category= await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id);
        var result= _mapper.Map<GetCategoryDto>(category);
        return Result<GetCategoryDto>.Success(result, "Category");
    }
}