namespace Databaser_Labb3_V2.Application.Navigation;

internal enum UserChoice
{
    GetPersonal,
    GetPersonalAll,
    GetPersonalTeachersOnly,
    GetPersonalAdminsOnly,
    GetPersonalLeadersOnly,

    GetStudents,
    GetStudentsAll,
    GetStudentsByClass,

    GetGradesLastMonth,

    GetAllCoursesWithGradeInfo,

    AddUser,
    AddStudent,
    AddPersonal,

    Back,
    Exit,

    Invalid
}
