using System.Threading.Tasks;
using System.Collections.Generic;
using Blazor_Notes.Models;

namespace Blazor_Notes.Services
{
    public interface INotesRepo
    {
        Task<bool> SaveAsync();

        // Note manipulation
        Task<bool> AddNoteAsync(Note note);
        Task<bool> EditNoteAsync(Note note);
        Task<bool> DeleteNoteAsync(Note note);

        // Note retrieving
        Task<IEnumerable<Note>> GetAllNotesAsync();
    }
}