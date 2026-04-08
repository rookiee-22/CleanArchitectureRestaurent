using Application.Interfaces.Repositories;
using Domain.Entities.Products;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Products.Commands;

public class DeleteProductCommand:IRequest<Result<string>>
{
    public int Id { get; set; }
    public DeleteProductCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<Product>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Product Deleted Succesfully.");
    }
}