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
        Task AddPersonalToDB(Personal personal);
        Task AddPersonalToDB(List<Personal> personal);
        Task AddStudentToDB(Studenter student);
        Task AddStudentToDB(List<Studenter> studenter);
        Task AssignStudentsToKlassList();
        Task<Dictionary<string, int>> GetTeachersInEveryDepartMent();
        Task<List<StudentInfo>> GetAllStudentInfo();
        Task<List<Ämnen>> GetAllActiveCourses();
        Task<List<DepartmentPayoutInformation>> GetDepartmentPayoutInformation();
        Task TESTKLASSLIST();
    }
}
