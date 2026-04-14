using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Helpers.Enums
{
    public enum MainMenuOption
    {
        Exit = 0,
        EducationMethods = 1,
        GroupMethods = 2,
        StudentMethods = 3
    }

    public enum EducationMenuOption
    {
        Create = 1,
        GetAll = 2,
        GetById = 3,
        Delete = 4,
        Update = 5,
        Search = 6,
        GetAllWithGroups = 7,
        SortWithCreatedDate = 8
    }

    public enum GroupMenuOption
    {
        Create = 1,
        GetAll = 2,
        GetById = 3,
        Delete = 4,
        Update = 5,
        Search = 6,
        FilterByEducationName = 7,
        GetAllWithEducationId = 8,
        SortWithCapacity = 9
    }

    public enum StudentMenuOption
    {
        Create = 1,
        GetAll = 2,
        GetById = 3,
        Delete = 4,
        Update = 5,
        Search = 6,
        GetAllWithGroupsAndEducations = 7,
        AddExistStudentToGroup = 8
    }
}
