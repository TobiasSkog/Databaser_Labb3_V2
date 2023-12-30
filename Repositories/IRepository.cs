using Databaser_Labb3_V2.Application.Navigation;
using Databaser_Labb3_V2.Models;

namespace Databaser_Labb3_V2.Repositories
{
    public interface IRepository
    {
        Task<List<Personal>> GetAllPersonal();
        Task<List<Studenter>> GetAllStudents();
        Task<List<string>> GetAllClassNames();
        Task<List<View_GetGradesFromLastMonth>> GetAllGradesLastMonth();
        Task<List<Personal>> GetAllPersonalsByRole(UserType userType);
        Task<List<KlassList>> GetAllStudentsInClass(string className, OrderOption nameSort, OrderOption ascOrDesc);
        Task<List<(string? Gender, int AgeGroup, double AverageGradeNumeric, Grade AverageGradeString)>> GetAverageGradesBasedByAgeAndGender();
        Task<List<CourseInformation>> GetCourseInformation();
        Task AddPersonalToDb(Personal personal);
        //Task AddPersonalToDb(List<Personal> personal);
        Task AddStudentToDb(Studenter student);
        //Task AddStudentToDb(List<Studenter> studenter);
        Task AssignStudentsToKlassList();
        Task<Dictionary<string, int>> GetTeachersInEveryDepartment();
        Task<List<StudentInfo>> GetAllStudentInfo();
        Task<List<Ämnen>> GetAllActiveCourses();
        Task<List<DepartmentPayoutInformation>> GetDepartmentPayoutInformation();
        Task TESTKLASSLIST();
    }
}
