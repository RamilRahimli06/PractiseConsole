using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Student Create(Student student);
        List<Student> GetAll();
        Student GetById(int id);
        bool Delete(int id);
        bool Update(Student student);
        List<Student> Search(string searchText);
        List<Student> GetAllWithGroupsAndEducations();
        bool AddExistStudentToGroup(int studentId, int groupId);
    }
}
