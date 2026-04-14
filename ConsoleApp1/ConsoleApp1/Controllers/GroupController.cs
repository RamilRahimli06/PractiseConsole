using Domain.Entities;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public void Create()
        {
            Console.Write("Group name: ");
            string name = Console.ReadLine();

            Console.Write("Capacity: ");
            bool capacityIsNumber = int.TryParse(Console.ReadLine(), out int capacity);
            if (!capacityIsNumber)
            {
                Console.WriteLine("Capacity must be number.");
                return;
            }

            Console.Write("Education id: ");
            bool educationIdIsNumber = int.TryParse(Console.ReadLine(), out int educationId);
            if (!educationIdIsNumber)
            {
                Console.WriteLine("Education id must be number.");
                return;
            }

            Group created = _groupService.Create(name, capacity, educationId);
            Console.WriteLine(created == null ? "Create failed." : $"Created. Id: {created.Id}");
        }

        public void GetAll()
        {
            List<Group> groups = _groupService.GetAll();
            if (groups.Count == 0)
            {
                Console.WriteLine("No group found.");
                return;
            }

            foreach (Group group in groups)
            {
                string educationName = group.Education == null ? "-" : group.Education.Name;
                Console.WriteLine($"Id: {group.Id} | Name: {group.Name} | Capacity: {group.Capacity} | Education: {educationName}");
            }
        }

        public void GetById()
        {
            Console.Write("Group id: ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int id);
            if (!isNumber)
            {
                Console.WriteLine("Id must be number.");
                return;
            }

            Group group = _groupService.GetById(id);
            if (group == null)
            {
                Console.WriteLine("Group not found.");
                return;
            }

            string educationName = group.Education == null ? "-" : group.Education.Name;
            Console.WriteLine($"Id: {group.Id} | Name: {group.Name} | Capacity: {group.Capacity} | Education: {educationName}");
        }

        public void Delete()
        {
            Console.Write("Group id: ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int id);
            if (!isNumber)
            {
                Console.WriteLine("Id must be number.");
                return;
            }

            bool deleted = _groupService.Delete(id);
            Console.WriteLine(deleted ? "Deleted." : "Delete failed.");
        }

        public void Update()
        {
            Console.Write("Group id: ");
            bool idIsNumber = int.TryParse(Console.ReadLine(), out int id);
            if (!idIsNumber)
            {
                Console.WriteLine("Id must be number.");
                return;
            }

            Console.Write("New name (empty -> keep old): ");
            string name = Console.ReadLine();

            Console.Write("New capacity (empty -> keep old): ");
            string capacityRaw = Console.ReadLine();
            int? capacity = null;
            if (!string.IsNullOrWhiteSpace(capacityRaw))
            {
                bool capacityIsNumber = int.TryParse(capacityRaw, out int parsedCapacity);
                if (!capacityIsNumber)
                {
                    Console.WriteLine("Capacity must be number.");
                    return;
                }

                capacity = parsedCapacity;
            }

            Console.Write("New education id (empty -> keep old): ");
            string educationIdRaw = Console.ReadLine();
            int? educationId = null;
            if (!string.IsNullOrWhiteSpace(educationIdRaw))
            {
                bool educationIdIsNumber = int.TryParse(educationIdRaw, out int parsedEducationId);
                if (!educationIdIsNumber)
                {
                    Console.WriteLine("Education id must be number.");
                    return;
                }

                educationId = parsedEducationId;
            }

            bool updated = _groupService.Update(id, name, capacity, educationId);
            Console.WriteLine(updated ? "Updated." : "Update failed.");
        }

        public void Search()
        {
            Console.Write("Search text: ");
            string searchText = Console.ReadLine();

            List<Group> groups = _groupService.Search(searchText);
            if (groups.Count == 0)
            {
                Console.WriteLine("No result.");
                return;
            }

            foreach (Group group in groups)
            {
                Console.WriteLine($"Id: {group.Id} | Name: {group.Name} | Capacity: {group.Capacity}");
            }
        }

        public void FilterByEducationName()
        {
            Console.Write("Education name: ");
            string educationName = Console.ReadLine();

            List<Group> groups = _groupService.FilterByEducationName(educationName);
            if (groups.Count == 0)
            {
                Console.WriteLine("No result.");
                return;
            }

            foreach (Group group in groups)
            {
                string name = group.Education == null ? "-" : group.Education.Name;
                Console.WriteLine($"Id: {group.Id} | Name: {group.Name} | Education: {name}");
            }
        }

        public void GetAllWithEducationId()
        {
            Console.Write("Education id: ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int educationId);
            if (!isNumber)
            {
                Console.WriteLine("Education id must be number.");
                return;
            }

            List<Group> groups = _groupService.GetAllWithEducationId(educationId);
            if (groups.Count == 0)
            {
                Console.WriteLine("No group found.");
                return;
            }

            foreach (Group group in groups)
            {
                Console.WriteLine($"Id: {group.Id} | Name: {group.Name} | Capacity: {group.Capacity} | EducationId: {group.EducationId}");
            }
        }

        public void SortWithCapacity()
        {
            Console.Write("Descending? (yes/no): ");
            string answer = Console.ReadLine();
            bool descending = answer != null && answer.Trim().ToLower() == "yes";

            List<Group> groups = _groupService.SortWithCapacity(descending);
            foreach (Group group in groups)
            {
                Console.WriteLine($"Id: {group.Id} | Name: {group.Name} | Capacity: {group.Capacity}");
            }
        }
    }
}
