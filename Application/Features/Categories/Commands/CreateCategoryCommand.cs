using Application.Commons.Mappings.Commons;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Categories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Categories.Commands;

public class CreateCategoryCommand:IRequest<Result<int>>,ICreateMapFrom<Category>
{
    public string Name { get; set; }
    public IFormFile Profile { get; set; }
    public string Description { get; set; }
}
internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileService = fileService;
    }
    public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var imageUrl = await _fileService.UploadAsync(request.Profile, "Product");
        var category = _mapper.Map<Category>(request);
        category.Profile = imageUrl;
        await _unitOfWork.Repository<Category>().CreateAsync(category);
        await _unitOfWork.Save(cancellationToken);
        return Result<int>.Success(category.Id, "Category Created Successfully");
    }
}
