using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniteds.CaseStudy.Domain.Models;

namespace Uniteds.CaseStudy.Domain.Interfaces
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetAllNotes();
        Note GetNoteById(int id);
        void AddNote(Note note);
        void UpdateNote(Note note);
        void DeleteNote(int id);
    }
}
