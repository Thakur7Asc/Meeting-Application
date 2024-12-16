namespace MeetingApplicationAPI.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeetingApplicationAPI.Repositories;
using MeetingApplicationAPI.Data;


    public class MeetingRepository : IMeetingRepository
    {
        private readonly ApplicationDbContext _context;

        public MeetingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Meeting>> GetAllMeetingsAsync()
        {
            return await _context.Meetings.ToListAsync();
        }

        public async Task<Meeting> GetMeetingByIdAsync(int id)
        {
            return await _context.Meetings.FindAsync(id);
        }

        public async Task AddMeetingAsync(Meeting meeting)
        {
            await _context.Meetings.AddAsync(meeting);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMeetingAsync(Meeting meeting)
        {
            _context.Meetings.Update(meeting);
            await _context.SaveChangesAsync();
        }

        //public async Task DeleteMeetingAsync(int id)
        //{
        //    var meeting = await _context.Meetings.FindAsync(id);
        //    if (meeting != null)
        //    {
        //        _context.Meetings.Remove(meeting);
        //        await _context.SaveChangesAsync();
        //    }
        //}

    public Task<IEnumerable<Meeting>> GetMeetingsAsync()
    {
        throw new NotImplementedException();
    }
}


