
using System;
using Microsoft.AspNetCore.Mvc;
using MeetingApplicationAPI.Data;
namespace MeetingApplicationAPI.Controllers;


[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(_context.Users.ToList());
    }
}

