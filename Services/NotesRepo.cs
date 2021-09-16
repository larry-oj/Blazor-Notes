using System.Threading.Tasks;
using System.Collections.Generic;
using Blazor_Notes.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Notes.Services
{
    public class NotesRepo : INotesRepo
    {
        private readonly NotesDbContext _context;

        public NotesRepo(NotesDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0 ? true : false;
        }


        public async Task<bool> AddNoteAsync(Note note)
        {
            await _context.Notes.AddAsync(note);

            return await SaveAsync();
        }

        public async Task<bool> EditNoteAsync(Note note)
        {
            _context.Notes.Update(note);

            return await SaveAsync();
        }

        public async Task<bool> DeleteNoteAsync(Note note)
        {
            _context.Notes.Remove(note);

            return await SaveAsync();
        }


        public IEnumerable<Note> GetAllNotes()
        {
            return _context.Notes.AsNoTracking().ToList();
        }
    }
}