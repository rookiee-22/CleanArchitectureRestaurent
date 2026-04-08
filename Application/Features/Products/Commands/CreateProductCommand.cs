using Application.Commons.Mappings.Commons;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Categories;
using Domain.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;
using Application.Interfaces.Repositories;


namespace Application.Features.Products.Commands;

public class CreateProductCommand:IRequest<Result<int>>,ICreateMapFrom<Product>
{
    public string Name { get; set; }
    public int Price { get; set; }
    public IFormFile Profile { get; set; }
    public string Description { get; set; }
    public int? CategoryId { get; set; }
}
internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IFileService fileService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (request.CategoryId.HasValue)
        {
            var categoryExists = await _unitOfWork.Repository<Category>().GetByIdAsync(request.CategoryId.Value);
            if (categoryExists == null)
            {
                return Result<int>.BadRequest("Category id not exit");
            }
        }
        var imageUrl = await _fileService.UploadAsync(request.Profile, "Product");
        var product= _mapper.Map<Product>(request);
        product.Profile = imageUrl;
        await _unitOfWork.Repository<Product>().CreateAsync(product);
        await _unitOfWork.Save(cancellationToken);
        return Result<int>.Success(product.Id, "Product Created Succesfully");
    }
}