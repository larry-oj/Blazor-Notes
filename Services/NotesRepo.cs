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


        // Idealy it should work async. However it conflicts: it attempts to use model before it is created.
        // It needs to await model creation, and only afterwards get data. It will take N amount of time
        // so i won't focus on it now
        public IEnumerable<Note> GetAllNotes()
        {
            return _context.Notes.AsNoTracking().ToList().OrderBy(n => n.Title);
        }
    }
}