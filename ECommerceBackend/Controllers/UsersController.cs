using AutoMapper;
using ECommerceBackend.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UsersController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        var userDtos = _mapper.Map<List<UserDto>>(users);
        return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        var userDto = _mapper.Map<UserDto>(user);
        return Ok(userDto);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
    {
        if (id != userDto.Id)
        {
            return BadRequest();
        }

        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _mapper.Map(userDto, user);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
