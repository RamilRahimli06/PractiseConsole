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
    public class EducationRepository : IEducationRepository
    {
        private readonly AppDbContext _context;

        public EducationRepository(AppDbContext context)
        {
            _context = context;
        }

        public Education Create(Education education)
        {
            _context.Educations.Add(education);
            _context.SaveChanges();
            return education;
        }

        public List<Education> GetAll()
        {
            return _context.Educations

                .ToList();
        }

        public Education GetById(int id)
        {
            return _context.Educations

      .FirstOrDefault(x => x.Id == id);
        }

        public bool Delete(int id)
        {
            Education education = GetById(id);
            if (education == null)
            {
                return false;
            }

            _context.Educations.Remove(education);
            _context.SaveChanges();
            return true;
        }

        public bool Update(Education education)
        {
            Education existingEducation = _context.Educations.FirstOrDefault(x => x.Id == education.Id);
            if (existingEducation == null)
            {
                return false;
            }

            existingEducation.Name = education.Name;
            existingEducation.Color = education.Color;
            _context.SaveChanges();
            return true;
        }

        public List<Education> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new List<Education>();
            }

            string normalizedSearch = searchText.Trim().ToLower();
            return _context.Educations

                .Where(x =>
                    (!string.IsNullOrWhiteSpace(x.Name) && x.Name.ToLower().Contains(normalizedSearch)) ||
                    (!string.IsNullOrWhiteSpace(x.Color) && x.Color.ToLower().Contains(normalizedSearch)))
                .ToList();
        }

        public List<EducationWithGroupsResult> GetAllWithGroups()
        {
            List<EducationWithGroupsResult> result = _context.Educations

                .Include(x => x.Groups)
                .Select(education => new EducationWithGroupsResult
                {
                    Education = education,
                    Groups = education.Groups.ToList()
                })
                .ToList();

            return result;
        }

        public List<Education> SortWithCreatedDate(bool descending)
        {
            return descending
                ? _context.Educations.AsNoTracking().OrderByDescending(x => x.CreatedDate).ToList()
                : _context.Educations.AsNoTracking().OrderBy(x => x.CreatedDate).ToList();
        }
    }
}
