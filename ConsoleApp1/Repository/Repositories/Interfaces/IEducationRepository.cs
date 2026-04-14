using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{

    public interface IEducationRepository
    {
        Education Create(Education education);
        List<Education> GetAll();
        Education GetById(int id);
        bool Delete(int id);
        bool Update(Education education);
        List<Education> Search(string searchText);
        List<EducationWithGroupsResult> GetAllWithGroups();
        List<Education> SortWithCreatedDate(bool descending);
    }
}
