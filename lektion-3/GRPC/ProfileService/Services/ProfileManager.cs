using Grpc.Core;
using ProfileService.Data;

namespace ProfileService.Services;

public class ProfileManager(DataContext context) : ProfileContract.ProfileContractBase
{
    private readonly DataContext _context = context;

    public override async Task<ProfileRegistrationReply> RegisterProfile(ProfileRegistrationRequest request, ServerCallContext context)
    {
        var profile = new ProfileEntity
        {
            UserId = request.UserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
        };

        _context.Add(profile);
        await _context.SaveChangesAsync();

        return new ProfileRegistrationReply
        {
            Succeeded = true,
            Message = "Profile was created"
        };
    }
}
