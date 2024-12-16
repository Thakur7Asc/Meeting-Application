using MeetingApplicationAPI.Models;

namespace MeetingApplicationAPI.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int userId);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int userId);

    // Method to get all meetings for a specific user
    Task<IEnumerable<Meeting>> GetMeetingsByUserIdAsync(int userId);

    // Method to add a user to a meeting
    Task AddUserToMeetingAsync(int userId, int meetingId);

    // Method to remove a user from a meeting
 //   Task RemoveUserFromMeetingAsync(int userId, int meetingId);
}


