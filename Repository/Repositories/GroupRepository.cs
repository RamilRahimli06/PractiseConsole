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
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _context;

        public GroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public Group Create(Group group)
        {
            _context.Groups.Add(group);
            _context.SaveChanges();
            return group;
        }
        public bool Delete(int id)
        {
            Group group = _context.Groups.FirstOrDefault(x => x.Id == id);
            if (group == null)
            {
                return false;
            }
            _context.Groups.Remove(group);
            _context.SaveChanges();
            return true;
        }
        public List<Group> FilterByEducationName(string educationName)
        {
            if (string.IsNullOrWhiteSpace(educationName))
            {
                return new List<Group>();
            }
            string normalizedEducationName = educationName.Trim().ToLower();
            return _context.Groups
               
                .Include(x => x.Education)
                .Where(x => x.Education != null && x.Education.Name.ToLower().Contains(normalizedEducationName))
                .ToList();
        }
        public List<Group> GetAll()
        {
            return _context.Groups
               
                .Include(x => x.Education)
                .ToList();
        }
        public List<Group> GetAllWithEducationId(int educationId)
        {
            return _context.Groups
            
                .Where(x => x.EducationId == educationId)
                .Include(x => x.Education)
                .ToList();
        }
        public Group GetById(int id)
        {
            return _context.Groups
               
                .Include(x => x.Education)
                .FirstOrDefault(x => x.Id == id);
        }
        public List<Group> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new List<Group>();
            }
            string normalizedSearch = searchText.Trim().ToLower();
            return _context.Groups
              
                .Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.ToLower().Contains(normalizedSearch))
                .ToList();
        }
        public List<Group> SortWithCapacity(bool descending)
        {
            return descending
                ? _context.Groups.OrderByDescending(x => x.Capacity).ToList()
                : _context.Groups.OrderBy(x => x.Capacity).ToList();
        }
        public bool Update(Group group)
        {
            Group existingGroup = _context.Groups.FirstOrDefault(x => x.Id == group.Id);
            if (existingGroup == null)
            {
                return false;
            }
            existingGroup.Name = group.Name;
            existingGroup.Capacity = group.Capacity;
            existingGroup.EducationId = group.EducationId;
            _context.SaveChanges();
            return true;
        }
    }

}

