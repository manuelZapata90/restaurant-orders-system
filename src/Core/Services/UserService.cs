
using Models.DTO_DB;
using Repositories;

namespace Services;

public class UserService
{
    private readonly UserRepository<UserDtoDb, int> _userRepo;

    public UserService(UserRepository<UserDtoDb, int> userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<UserDtoDb?> GetUser(int id)
    {
        return await _userRepo.GetByIdAsync(id);
    }
}