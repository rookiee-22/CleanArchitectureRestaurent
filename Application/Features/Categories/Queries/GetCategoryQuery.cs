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

public class GetCategoryQuery:IRequest<Result<List<GetCategoryDto>>>
{

}
internal class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Result<List<GetCategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetCategoryDto>>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
        var result = _mapper.Map<List<GetCategoryDto>>(categories);
        return Result<List<GetCategoryDto>>.Success(result, "Categories");
    }
}
