namespace MeetingApplicationAPI.Controllers;

using System;
using Microsoft.AspNetCore.Mvc;
using MeetingApplicationAPI.Data;

[Route("api/meetings")]
[ApiController]
public class MeetingsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MeetingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetMeetings()
    {
        return Ok(_context.Meetings.ToList());
    }
}

