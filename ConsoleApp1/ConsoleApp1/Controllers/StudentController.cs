using Domain.Entities;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controllers
{
    public class StudentController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public void Create()
        {
            Console.Write("Full name: ");
            string fullName = Console.ReadLine();

            Console.Write("Age: ");
            bool ageIsNumber = int.TryParse(Console.ReadLine(), out int age);
            if (!ageIsNumber)
            {
                Console.WriteLine("Age must be number.");
                return;
            }

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("Group id (empty allowed): ");
            string groupIdRaw = Console.ReadLine();
            int? groupId = null;
            if (!string.IsNullOrWhiteSpace(groupIdRaw))
            {
                bool groupIdIsNumber = int.TryParse(groupIdRaw, out int parsedGroupId);
                if (!groupIdIsNumber)
                {
                    Console.WriteLine("Group id must be number.");
                    return;
                }

                groupId = parsedGroupId;
            }

            (Student created, string createError) = _studentService.Create(fullName, age, email, address, groupId);
            if (created == null)
            {
                Console.WriteLine(createError ?? "Create failed.");
                return;
            }

            Console.WriteLine($"Created. Id: {created.Id}");
        }

        public void GetAll()
        {
            List<Student> students = _studentService.GetAll();
            if (students.Count == 0)
            {
                Console.WriteLine("No student found.");
                return;
            }

            foreach (Student student in students)
            {
                Console.WriteLine($"Id: {student.Id} | FullName: {student.FullName} | Age: {student.Age} | Email: {student.Email} | GroupId: {student.GroupId}");
            }
        }

        public void GetById()
        {
            Console.Write("Student id: ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int id);
            if (!isNumber)
            {
                Console.WriteLine("Id must be number.");
                return;
            }

            Student student = _studentService.GetById(id);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.WriteLine($"Id: {student.Id} | FullName: {student.FullName} | Age: {student.Age} | Email: {student.Email} | GroupId: {student.GroupId}");
        }

        public void Delete()
        {
            Console.Write("Student id: ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int id);
            if (!isNumber)
            {
                Console.WriteLine("Id must be number.");
                return;
            }

            bool deleted = _studentService.Delete(id);
            Console.WriteLine(deleted ? "Deleted." : "Delete failed.");
        }

        public void Update()
        {
            Console.Write("Student id: ");
            bool idIsNumber = int.TryParse(Console.ReadLine(), out int id);
            if (!idIsNumber)
            {
                Console.WriteLine("Id must be number.");
                return;
            }

            Console.Write("New full name (empty -> keep old): ");
            string fullName = Console.ReadLine();

            Console.Write("New age (empty -> keep old): ");
            string ageRaw = Console.ReadLine();
            int? age = null;
            if (!string.IsNullOrWhiteSpace(ageRaw))
            {
                bool ageIsNumber = int.TryParse(ageRaw, out int parsedAge);
                if (!ageIsNumber)
                {
                    Console.WriteLine("Age must be number.");
                    return;
                }

                age = parsedAge;
            }

            Console.Write("New email (empty -> keep old): ");
            string email = Console.ReadLine();

            Console.Write("New address (empty -> keep old): ");
            string address = Console.ReadLine();

            Console.Write("New group id (empty -> keep old, 0 -> remove group): ");
            string groupIdRaw = Console.ReadLine();
            int? groupId = null;
            if (!string.IsNullOrWhiteSpace(groupIdRaw))
            {
                bool groupIdIsNumber = int.TryParse(groupIdRaw, out int parsedGroupId);
                if (!groupIdIsNumber)
                {
                    Console.WriteLine("Group id must be number.");
                    return;
                }

                groupId = parsedGroupId;
            }

            (bool updated, string updateError) = _studentService.Update(id, fullName, age, email, address, groupId);
            Console.WriteLine(updated ? "Updated." : (updateError ?? "Update failed."));
        }

        public void Search()
        {
            Console.Write("Search text: ");
            string searchText = Console.ReadLine();

            List<Student> students = _studentService.Search(searchText);
            if (students.Count == 0)
            {
                Console.WriteLine("No result.");
                return;
            }

            foreach (Student student in students)
            {
                Console.WriteLine($"Id: {student.Id} | FullName: {student.FullName} | Email: {student.Email}");
            }
        }

        public void GetAllWithGroupsAndEducations()
        {
            List<Student> students = _studentService.GetAllWithGroupsAndEducations();
            if (students.Count == 0)
            {
                Console.WriteLine("No student found.");
                return;
            }

            foreach (Student student in students)
            {
                string groupName = student.Group == null ? "-" : student.Group.Name;
                string educationName = student.Group?.Education == null ? "-" : student.Group.Education.Name;
                Console.WriteLine($"Id: {student.Id} | FullName: {student.FullName} | Group: {groupName} | Education: {educationName}");
            }
        }

        public void AddExistStudentToGroup()
        {
            Console.Write("Student id: ");
            bool studentIdIsNumber = int.TryParse(Console.ReadLine(), out int studentId);
            if (!studentIdIsNumber)
            {
                Console.WriteLine("Student id must be number.");
                return;
            }

            Console.Write("Group id: ");
            bool groupIdIsNumber = int.TryParse(Console.ReadLine(), out int groupId);
            if (!groupIdIsNumber)
            {
                Console.WriteLine("Group id must be number.");
                return;
            }

            bool updated = _studentService.AddExistStudentToGroup(studentId, groupId);
            Console.WriteLine(updated ? "Student added to group." : "Operation failed.");
        }
    }
}
