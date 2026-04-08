using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Profiles;
using MediatR;
using Shared;
using DomainProfile = Domain.Entities.Profiles.Profile;

namespace Application.Features.Profiles.Commands;

public class UpdateProfileCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public CreateProfileCommand CreateProfile { get; set; }
    public UpdateProfileCommand(int id, CreateProfileCommand createProfile)
    {
        Id = id;
        CreateProfile = createProfile;
    }
}
internal class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProfileCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateProfile.UserId != 0)
        {
            var user = await _unitOfWork.Repository<Domain.Entities.Users.User>().GetByIdAsync(request.CreateProfile.UserId);
            if (user == null)
            {
                return Result<string>.BadRequest("User Id is not exist.");
            }
        }

        var profile = await _unitOfWork.Repository<DomainProfile>().GetByIdAsync(request.Id);
        if (profile == null)
        {
            return Result<string>.BadRequest("Profile Not Found");
        }
        _mapper.Map(request.CreateProfile, profile);
        await _unitOfWork.Repository<DomainProfile>().UpdateAsync(profile);
        await _unitOfWork.Save(cancellationToken);
        return Result<string>.Success("Profile Updated Succesfully");
    }
}
