using Application.Commons.Mappings.Commons;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Profiles;
using MediatR;
using Shared;
using DomainProfile = Domain.Entities.Profiles.Profile;

namespace Application.Features.Profiles.Commands;

public class CreateProfileCommand : IRequest<Result<int>>, ICreateMapFrom<DomainProfile>
{
    public string ImageUrl { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public int UserId { get; set; }
}
internal class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProfileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<Domain.Entities.Users.User>().GetByIdAsync(request.UserId);
        if (user == null)
        {
            return Result<int>.BadRequest("User id not exist");
        }

        var profile = _mapper.Map<DomainProfile>(request);
        await _unitOfWork.Repository<DomainProfile>().CreateAsync(profile);
        await _unitOfWork.Save(cancellationToken);
        return Result<int>.Success(profile.Id, "Profile Created Successfully");
    }
}
