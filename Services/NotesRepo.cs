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
        // I assume it needs to await model creation, and only afterwards get data. I have never ever encountered this
        // problem before so right now I am clueless why does this happen, as it always worked perfectrly fine
        // with MySQL databases. It will take N amount of time to find a fix for it so I will leave it as is for now
        public List<Note> GetAllNotes()
        {
            return _context.Notes.AsNoTracking().ToList();
        }
    }
}