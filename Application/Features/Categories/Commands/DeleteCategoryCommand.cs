using Application.Interfaces.Repositories;
using Domain.Entities.Categories;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Categories.Commands;

public class DeleteCategoryCommand:IRequest<Result<string>>
{
    public int Id { get; set; }
    public DeleteCategoryCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<Category>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Category deleted Succesfully");
    }
}