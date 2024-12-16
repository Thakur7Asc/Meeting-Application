using MeetingApplicationAPI.Models;

namespace MeetingApplicationAPI.Repositories;

public interface IMeetingRepository
{
    Task<IEnumerable<Meeting>> GetMeetingsAsync();
    Task<Meeting> GetMeetingByIdAsync(int meetingId);
    Task AddMeetingAsync(Meeting meeting);
    Task UpdateMeetingAsync(Meeting meeting);
    //Task DeleteMeetingAsync(int meetingId);
    // Add other methods specific to meetings if needed
}

