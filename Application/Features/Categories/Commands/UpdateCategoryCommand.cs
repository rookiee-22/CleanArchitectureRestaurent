using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Categories;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Categories.Commands;

public class UpdateCategoryCommand:IRequest<Result<string>>
{
    public int Id { get; set; }
    public CreateCategoryCommand CreateCategory {  get; set; }
    public UpdateCategoryCommand(int id,CreateCategoryCommand createCategory)
    {
        Id = id;
        CreateCategory = createCategory;

    }
}
internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public UpdateCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IFileService fileService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<Result<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id);
        if (category == null)
        {
            return Result<string>.BadRequest("Category not found");
        }
        _mapper.Map(request.CreateCategory, category);
        if(request.CreateCategory.Profile != null)
        {
            var imageUrl = await _fileService.UploadAsync(request.CreateCategory.Profile, "Category");
            category.Profile = imageUrl;
        }
        await _unitOfWork.Repository<Category>().UpdateAsync(category);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Category updated Succesfully");
    }
}