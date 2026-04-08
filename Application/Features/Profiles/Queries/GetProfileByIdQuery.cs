using Application.Dtos.Profiles;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Profiles;
using MediatR;
using Shared;
using DomainProfile = Domain.Entities.Profiles.Profile;

namespace Application.Features.Profiles.Queries;

public class GetProfileByIdQuery : IRequest<Result<GetProfileDto>>
{
    public int Id { get; set; }
    public GetProfileByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetProfileByIdQueryHandler : IRequestHandler<GetProfileByIdQuery, Result<GetProfileDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProfileByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetProfileDto>> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var profile = await _unitOfWork.Repository<DomainProfile>().GetByIdAsync(request.Id);
        var result = _mapper.Map<GetProfileDto>(profile);
        return Result<GetProfileDto>.Success(result, "Profile");
    }
}
