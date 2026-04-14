using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Group Create(Group group);
        List<Group> GetAll();
        Group GetById(int id);
        bool Delete(int id);
        bool Update(Group group);
        List<Group> Search(string searchText);
        List<Group> FilterByEducationName(string educationName);
        List<Group> GetAllWithEducationId(int educationId);
        List<Group> SortWithCapacity(bool descending);
    }
}
