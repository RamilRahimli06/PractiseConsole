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
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IEducationRepository _educationRepository;

        public GroupService(IGroupRepository groupRepository, IEducationRepository educationRepository)
        {
            _groupRepository = groupRepository;
            _educationRepository = educationRepository;
        }

        public Group Create(string name, int capacity, int educationId)
        {
            if (string.IsNullOrWhiteSpace(name) || capacity <= 0 || educationId <= 0)
            {
                return null;
            }

            Education education = _educationRepository.GetById(educationId);
            if (education == null)
            {
                return null;
            }

            Group group = new Group
            {
                Name = name.Trim(),
                Capacity = capacity,
                EducationId = educationId
            };

            return _groupRepository.Create(group);
        }

        public List<Group> GetAll()
        {
            return _groupRepository.GetAll();
        }

        public Group GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return _groupRepository.GetById(id);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            return _groupRepository.Delete(id);
        }

        public bool Update(int id, string name, int? capacity, int? educationId)
        {
            if (id <= 0)
            {
                return false;
            }

            Group existingGroup = _groupRepository.GetById(id);
            if (existingGroup == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                existingGroup.Name = name.Trim();
            }

            if (capacity.HasValue && capacity.Value > 0)
            {
                existingGroup.Capacity = capacity.Value;
            }

            if (educationId.HasValue && educationId.Value > 0)
            {
                Education education = _educationRepository.GetById(educationId.Value);
                if (education == null)
                {
                    return false;
                }

                existingGroup.EducationId = educationId.Value;
            }

            return _groupRepository.Update(existingGroup);
        }

        public List<Group> Search(string searchText)
        {
            return _groupRepository.Search(searchText);
        }

        public List<Group> FilterByEducationName(string educationName)
        {
            return _groupRepository.FilterByEducationName(educationName);
        }

        public List<Group> GetAllWithEducationId(int educationId)
        {
            if (educationId <= 0)
            {
                return new List<Group>();
            }

            return _groupRepository.GetAllWithEducationId(educationId);
        }

        public List<Group> SortWithCapacity(bool descending)
        {
            return _groupRepository.SortWithCapacity(descending);
        }
    }
}
