//namespace MeetingApplicationAPI.Models;

//public class Meeting
//{
//    public int Id { get; set; }
//    public string Title { get; set; }
//    public string Description { get; set; }
//    public DateTime Date { get; set; }
//    public ICollection<UserMeeting> UserMeetings { get; set; }
//}

using MeetingApplicationAPI.Models;

public class Meeting
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime ScheduledAt { get; set; }

    public ICollection<UserMeeting> UserMeetings { get; set; }
}
