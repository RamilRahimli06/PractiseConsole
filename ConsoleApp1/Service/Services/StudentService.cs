using Domain.Common;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;

        public StudentService(IStudentRepository studentRepository, IGroupRepository groupRepository)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
        }

        public (Student Student, string ErrorMessage) Create(string fullName, int age, string email, string address, int? groupId)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(address))
            {
                return (null, "Required fields cannot be empty.");
            }

            if (!ValidationHelper.FullNameHasNoDigits(fullName))
            {
                return (null, "Full name cannot contain digits.");
            }

            if (!ValidationHelper.EmailHasValidAt(email))
            {
                return (null, "Email must contain @ with text before and after it.");
            }

            if (!ValidationHelper.IsAgeInRange(age, 15, 50))
            {
                return (null, "Age must be between 15 and 50.");
            }

            if (groupId.HasValue && groupId.Value > 0)
            {
                Group group = _groupRepository.GetById(groupId.Value);
                if (group == null)
                {
                    return (null, "Group not found.");
                }
            }

            Student student = new Student
            {
                FullName = fullName.Trim(),
                Age = age,
                Email = email.Trim(),
                Address = address.Trim(),
                GroupId = groupId
            };

            return (_studentRepository.Create(student), null);
        }

        public List<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public Student GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return _studentRepository.GetById(id);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            return _studentRepository.Delete(id);
        }

        public (bool Success, string ErrorMessage) Update(int id, string fullName, int? age, string email, string address, int? groupId)
        {
            if (id <= 0)
            {
                return (false, "Invalid id.");
            }

            Student existingStudent = _studentRepository.GetById(id);
            if (existingStudent == null)
            {
                return (false, "Student not found.");
            }

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                if (!ValidationHelper.FullNameHasNoDigits(fullName))
                {
                    return (false, "Full name cannot contain digits.");
                }

                existingStudent.FullName = fullName.Trim();
            }

            if (age.HasValue)
            {
                if (!ValidationHelper.IsAgeInRange(age.Value, 15, 50))
                {
                    return (false, "Age must be between 15 and 50.");
                }

                existingStudent.Age = age.Value;
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                if (!ValidationHelper.EmailHasValidAt(email))
                {
                    return (false, "Email must contain @ with text before and after it.");
                }

                existingStudent.Email = email.Trim();
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                existingStudent.Address = address.Trim();
            }

            if (groupId.HasValue)
            {
                if (groupId.Value > 0)
                {
                    Group group = _groupRepository.GetById(groupId.Value);
                    if (group == null)
                    {
                        return (false, "Group not found.");
                    }
                }

                existingStudent.GroupId = groupId.Value <= 0 ? null : groupId;
            }

            bool updated = _studentRepository.Update(existingStudent);
            return updated ? (true, null) : (false, "Update failed.");
        }

        public List<Student> Search(string searchText)
        {
            return _studentRepository.Search(searchText);
        }

        public List<Student> GetAllWithGroupsAndEducations()
        {
            return _studentRepository.GetAllWithGroupsAndEducations();
        }

        public bool AddExistStudentToGroup(int studentId, int groupId)
        {
            if (studentId <= 0 || groupId <= 0)
            {
                return false;
            }

            return _studentRepository.AddExistStudentToGroup(studentId, groupId);
        }
    }
}
