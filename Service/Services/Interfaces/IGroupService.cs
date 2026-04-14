using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        Group Create(string name, int capacity, int educationId);
        List<Group> GetAll();
        Group GetById(int id);
        bool Delete(int id);
        bool Update(int id, string name, int? capacity, int? educationId);
        List<Group> Search(string searchText);
        List<Group> FilterByEducationName(string educationName);
        List<Group> GetAllWithEducationId(int educationId);
        List<Group> SortWithCapacity(bool descending);
    }
}
