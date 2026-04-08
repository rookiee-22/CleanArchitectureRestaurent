using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Categories;
using Domain.Entities.Products;
using MediatR;
using Shared;

namespace Application.Features.Products.Commands;

public class UpdateProductCommand:IRequest<Result<string>>
{
    public int Id { get; set; }
    public CreateProductCommand CreateProduct {  get; set; }
    public UpdateProductCommand(int id,CreateProductCommand createProduct)
    {
        Id=id;
        CreateProduct = createProduct;
    }
}
internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IFileService fileService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateProduct.CategoryId.HasValue)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.CreateProduct.CategoryId.Value);

            if (category == null)
            {
                return Result<string>.BadRequest("Category Id is not exist.");
            }
        }
        
        var product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);
        if (product == null)
        {
            return Result<string>.BadRequest("Product Not Found");
        }

        _mapper.Map(request.CreateProduct, product);
        
       
        if (request.CreateProduct.Profile != null)
        {
            var imageUrl = await _fileService.UploadAsync(request.CreateProduct.Profile, "Product");
            product.Profile = imageUrl;
        }
        
        await _unitOfWork.Repository<Product>().UpdateAsync(product);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Product Updated Succesfully");
    }
}