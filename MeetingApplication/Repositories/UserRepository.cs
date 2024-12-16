namespace MeetingApplicationAPI.Repositories;
using MeetingApplicationAPI.Data;
using MeetingApplicationAPI.Models;
using Microsoft.EntityFrameworkCore;
using MeetingApplicationAPI.Models;  // Use the appropriate namespace for your models


    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all users
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();  // Fetch all users from the Users table
        }

        // Get a user by their ID
        public async Task<User> GetUserByIdAsync(int userId)
        {
        return await _context.Users
            .Include(u => u.UserMeetings)  // Include the UserMeetings relationship
            .ThenInclude(um => um.Meeting)  // Include related Meetings
                .FirstOrDefaultAsync(u => u.Id == userId.ToString());  // Find the user by ID
    }

        // Add a new user
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);  // Add the user to the Users table
            await _context.SaveChangesAsync();  // Save changes to the database
        }

        // Update an existing user
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);  // Update the user entity
            await _context.SaveChangesAsync();  // Save changes to the database
        }

        // Delete a user
        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);  // Find the user by ID
            if (user != null)
            {
                _context.Users.Remove(user);  // Remove the user from the Users table
                await _context.SaveChangesAsync();  // Save changes to the database
            }
        }

        // Get all meetings for a specific user
        public async Task<IEnumerable<Meeting>> GetMeetingsByUserIdAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserMeetings)  // Include UserMeetings
                .ThenInclude(um => um.Meeting)  // Include Meetings
                .FirstOrDefaultAsync(u => u.Id == userId.ToString());  // Find the user by ID

        return user?.UserMeetings.Select(um => um.Meeting) ?? new List<Meeting>();  // Return user's meetings
        }

        // Add a user to a meeting
        public async Task AddUserToMeetingAsync(int userId, int meetingId)
        {
            var user = await _context.Users.FindAsync(userId);  // Find the user by ID
            var meeting = await _context.Meetings.FindAsync(meetingId);  // Find the meeting by ID

            if (user != null && meeting != null)
            {
                var userMeeting = new UserMeeting { UserId = userId.ToString(), MeetingId = meetingId };
                _context.UserMeetings.Add(userMeeting);  // Add the user to the meeting
                await _context.SaveChangesAsync();  // Save changes to the database
            }
        }

        //// Remove a user from a meeting
        //public async Task RemoveUserFromMeetingAsync(int userId, int meetingId)
        //{
        //    var userMeeting = await _context.Meetings
        //        .FirstOrDefaultAsync(um => um.Id == userId && um.UserMeetings == meetingId);  // Find the UserMeeting record

        //    if (userMeeting != null)
        //    {
        //        _context.UserMeetings.Remove(userMeeting);  // Remove the user from the meeting
        //        await _context.SaveChangesAsync();  // Save changes to the database
        //    }
        //}
    }


