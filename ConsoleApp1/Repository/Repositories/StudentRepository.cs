using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public Student Create(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public List<Student> GetAll()
        {
            return _context.Students
               
                .ToList();
        }

        public Student GetById(int id)
        {
            return _context.Students
               
                .FirstOrDefault(x => x.Id == id);
        }

        public bool Delete(int id)
        {
            Student student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return false;
            }

            _context.Students.Remove(student);
            _context.SaveChanges();
            return true;
        }

        public bool Update(Student student)
        {
            Student existingStudent = _context.Students.FirstOrDefault(x => x.Id == student.Id);
            if (existingStudent == null)
            {
                return false;
            }

            existingStudent.FullName = student.FullName;
            existingStudent.Age = student.Age;
            existingStudent.Email = student.Email;
            existingStudent.Address = student.Address;
            existingStudent.GroupId = student.GroupId;
            _context.SaveChanges();
            return true;
        }

        public List<Student> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new List<Student>();
            }

            string normalizedSearch = searchText.Trim().ToLower();
            return _context.Students
                
                .Where(x =>
                    (!string.IsNullOrWhiteSpace(x.FullName) && x.FullName.ToLower().Contains(normalizedSearch)) ||
                    (!string.IsNullOrWhiteSpace(x.Email) && x.Email.ToLower().Contains(normalizedSearch)) ||
                    (!string.IsNullOrWhiteSpace(x.Address) && x.Address.ToLower().Contains(normalizedSearch)))
                .ToList();
        }

        public List<Student> GetAllWithGroupsAndEducations()
        {
            return _context.Students
               
                .Include(x => x.Group)
                .ThenInclude(x => x.Education)
                .ToList();
        }

        public bool AddExistStudentToGroup(int studentId, int groupId)
        {
            Student student = _context.Students.FirstOrDefault(x => x.Id == studentId);
            if (student == null)
            {
                return false;
            }

            Group group = _context.Groups.FirstOrDefault(x => x.Id == groupId);
            if (group == null)
            {
                return false;
            }

            student.GroupId = groupId;
            _context.SaveChanges();
            return true;
        }
    }
}
