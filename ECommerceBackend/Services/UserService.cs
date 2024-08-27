using AutoMapper;
using ECommerceBackend.Middleware;
using ECommerceBackend.Repositories;

namespace ECommerceBackend.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(int id);
    Task AddUserAsync(UserDto userDto);
    Task UpdateUserAsync(int id, UserDto userDto);
    Task DeleteUserAsync(int id);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return null;
        }
        return _mapper.Map<UserDto>(user);
    }

    public async Task AddUserAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _userRepository.AddAsync(user);
    }

    public async Task UpdateUserAsync(int id, UserDto userDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return;
        }

        _mapper.Map(userDto, user);
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
    }
}
