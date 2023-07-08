using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniteds.CaseStudy.Domain.Interfaces;
using Uniteds.CaseStudy.Domain.Models;
using Uniteds.CaseStudy.Repository.Context;

namespace Uniteds.CaseStudy.Repository.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _dbContext;

        public NoteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return _dbContext.Notes.Where(n => !n.IsDeleted).ToList();
        }

        public Note GetNoteById(int id)
        {
            return _dbContext.Notes.FirstOrDefault(n => n.Id == id && !n.IsDeleted);
        }

        public void AddNote(Note note)
        {
            _dbContext.Notes.Add(note);
            _dbContext.SaveChanges();
        }

        public void UpdateNote(Note note)
        {
            _dbContext.Notes.Update(note);
            _dbContext.SaveChanges();
        }

        public void DeleteNote(int id)
        {
            var note = _dbContext.Notes.FirstOrDefault(n => n.Id == id);

            if (note != null)
            {
                note.IsDeleted = true;
                _dbContext.SaveChanges();
            }
        }
    
    }
}
