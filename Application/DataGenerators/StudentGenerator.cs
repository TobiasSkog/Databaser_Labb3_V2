using Databaser_Labb3_V2.Models;

namespace Databaser_Labb3_V2.Application.DataGenerators;
public static class StudentGenerator
{
    public static List<Studenter> GenerateStudents(int maleCount, int femaleCount)
    {
        var students = new List<Studenter>();

        students.AddRange(GenerateStudentsByGender(maleCount, 'M'));
        students.AddRange(GenerateStudentsByGender(femaleCount, 'F'));

        return students;
    }

    private static List<Studenter> GenerateStudentsByGender(int count, char gender)
    {
        var students = new List<Studenter>();

        for (int i = 0; i < count; i++)
        {
            string firstName = gender == 'M' ? DataLists.GetMaleFirstName() : DataLists.GetFemaleFirstName();
            string lastName = DataLists.GetLastName();
            string studentNamn = $"{firstName} {lastName}";
            string studentSsn = DataLists.GenerateRandomSsn(gender);

            students.Add(new Studenter { StudentNamn = studentNamn, StudentSsn = studentSsn });
        }

        return students;
    }
}
