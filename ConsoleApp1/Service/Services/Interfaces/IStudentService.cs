using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IStudentService
    {
        (Student Student, string ErrorMessage)  Create(string fullName, int age, string email, string address, int? groupId);
        List<Student> GetAll();
        Student GetById(int id);
        bool Delete(int id);
        (bool Success, string ErrorMessage) Update(int id, string fullName, int? age, string email, string address, int? groupId);
        List<Student> Search(string searchText);
        List<Student> GetAllWithGroupsAndEducations();
        bool AddExistStudentToGroup(int studentId, int groupId);
    }
}
