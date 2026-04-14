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
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;

        public EducationService(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public Education Create(string name, string color)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(color))
            {
                return null;
            }

            Education education = new Education
            {
                Name = name.Trim(),
                Color = color.Trim()
            };

            return _educationRepository.Create(education);
        }

        public List<Education> GetAll()
        {
            return _educationRepository.GetAll();
        }

        public Education GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return _educationRepository.GetById(id);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            return _educationRepository.Delete(id);
        }

        public bool Update(int id, string name, string color)
        {
            if (id <= 0)
            {
                return false;
            }

            Education existingEducation = _educationRepository.GetById(id);
            if (existingEducation == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                existingEducation.Name = name.Trim();
            }

            if (!string.IsNullOrWhiteSpace(color))
            {
                existingEducation.Color = color.Trim();
            }

            return _educationRepository.Update(existingEducation);
        }

        public List<Education> Search(string searchText)
        {
            return _educationRepository.Search(searchText);
        }

        public List<EducationWithGroupsResult> GetAllWithGroups()
        {
            return _educationRepository.GetAllWithGroups();
        }

        public List<Education> SortWithCreatedDate(bool descending)
        {
            return _educationRepository.SortWithCreatedDate(descending);
        }
    }
}
