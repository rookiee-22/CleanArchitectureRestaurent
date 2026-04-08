using Application.Dtos.Profiles;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Profiles;
using MediatR;
using Shared;
using DomainProfile = Domain.Entities.Profiles.Profile;

namespace Application.Features.Profiles.Queries;

public class GetProfilesQuery : IRequest<Result<List<GetProfileDto>>>
{
}
internal class GetProfilesQueryHandler : IRequestHandler<GetProfilesQuery, Result<List<GetProfileDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProfilesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetProfileDto>>> Handle(GetProfilesQuery request, CancellationToken cancellationToken)
    {
        var profiles = await _unitOfWork.Repository<DomainProfile>().GetAllAsync();
        var result = _mapper.Map<List<GetProfileDto>>(profiles);
        return Result<List<GetProfileDto>>.Success(result, "Profiles");
    }
}
