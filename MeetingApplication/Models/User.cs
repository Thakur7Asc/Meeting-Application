//namespace MeetingApplicationAPI.Models;

//public class User
//{
//    public int Id { get; set; }
//    public string Username { get; set; }
//    public string Password { get; set; }
//    public string Email { get; set; }
//    public ICollection<UserMeeting> UserMeetings { get; set; }
//}

using MeetingApplicationAPI.Models;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public ICollection<UserMeeting> UserMeetings { get; set; }
}