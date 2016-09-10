using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models.Repositories
{
    public interface IInstructorRepository : IDisposable
    {
        IEnumerable<Instructor> GetInstructors();
        Instructor GetInstructorByID(int instructorID);
        void InsertInstructor(Instructor instructor);
        void DeleteInstructor(int instructorID);
        void UpdateInstructor(Instructor instructor);
        void Save();
    }
}
