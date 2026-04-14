using Domain.Entities;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controllers
{
    public class EducationController
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        public void Create()
        {
            Console.Write("Education name: ");
            string name = Console.ReadLine();

            Console.Write("Education color: ");
            string color = Console.ReadLine();

            Education created = _educationService.Create(name, color);
            Console.WriteLine(created == null ? "Create failed." : $"Created. Id: {created.Id}");
        }

        public void GetAll()
        {
            List<Education> educations = _educationService.GetAll();
            if (educations.Count == 0)
            {
                Console.WriteLine("No education found.");
                return;
            }

            foreach (Education education in educations)
            {
                Console.WriteLine($"Id: {education.Id} | Name: {education.Name} | Color: {education.Color} | Created: {education.CreatedDate}");
            }
        }

        public void GetById()
        {
            Console.Write("Education id: ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int id);
            if (!isNumber)
            {
                Console.WriteLine("Id must be number.");
                return;
            }

            Education education = _educationService.GetById(id);
            Console.WriteLine(education == null
                ? "Education not found."
                : $"Id: {education.Id} | Name: {education.Name} | Color: {education.Color} | Created: {education.CreatedDate}");
        }

        public void Delete()
        {
            Console.Write("Education id: ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int id);
            if (!isNumber)
            {
                Console.WriteLine("Id must be number.");
                return;
            }

            bool deleted = _educationService.Delete(id);
            Console.WriteLine(deleted ? "Deleted." : "Delete failed.");
        }

        public void Update()
        {
            Console.Write("Education id: ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int id);
            if (!isNumber)
            {
                Console.WriteLine("Id must be number.");
                return;
            }

            Console.Write("New name (empty -> keep old): ");
            string name = Console.ReadLine();

            Console.Write("New color (empty -> keep old): ");
            string color = Console.ReadLine();

            bool updated = _educationService.Update(id, name, color);
            Console.WriteLine(updated ? "Updated." : "Update failed.");
        }

        public void Search()
        {
            Console.Write("Search text: ");
            string searchText = Console.ReadLine();

            List<Education> result = _educationService.Search(searchText);
            if (result.Count == 0)
            {
                Console.WriteLine("No result.");
                return;
            }

            foreach (Education education in result)
            {
                Console.WriteLine($"Id: {education.Id} | Name: {education.Name} | Color: {education.Color} | Created: {education.CreatedDate}");
            }
        }

        public void GetAllWithGroups()
        {
            List<EducationWithGroupsResult> items = _educationService.GetAllWithGroups();
            if (items.Count == 0)
            {
                Console.WriteLine("No education found.");
                return;
            }

            foreach (EducationWithGroupsResult item in items)
            {
                Console.WriteLine($"Education -> Id: {item.Education.Id} | Name: {item.Education.Name} | Groups: {item.Groups.Count}");
            }
        }

        public void SortWithCreatedDate()
        {
            Console.Write("Descending? (yes/no): ");
            string answer = Console.ReadLine();
            bool descending = answer != null && answer.Trim().ToLower() == "yes";

            List<Education> result = _educationService.SortWithCreatedDate(descending);
            foreach (Education education in result)
            {
                Console.WriteLine($"Id: {education.Id} | Name: {education.Name} | Created: {education.CreatedDate}");
            }
        }
    }
}
