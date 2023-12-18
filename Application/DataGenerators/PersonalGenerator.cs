using Databaser_Labb3_V2.Models;

namespace Databaser_Labb3_V2.Application.DataGenerators;

public class PersonalGenerator
{
    public static List<Personal> GeneratePersonal(int maleCount, int femaleCount)
    {
        var personal = new List<Personal>();

        personal.AddRange(GeneratePersonalByGender(maleCount, 'M'));
        personal.AddRange(GeneratePersonalByGender(femaleCount, 'F'));

        return personal;
    }

    private static List<Personal> GeneratePersonalByGender(int count, char gender)
    {
        var personal = new List<Personal>();

        for (int i = 0; i < count; i++)
        {
            string firstName = gender == 'M' ? DataLists.GetMaleFirstName() : DataLists.GetFemaleFirstName();
            string lastName = DataLists.GetLastName();
            string personalNamn = $"{firstName} {lastName}";
            string personalSsn = DataLists.GenerateRandomSsn(gender);

            personal.Add(new Personal
            {
                PersonalNamn = personalNamn,
                PersonalSsn = personalSsn,
                PersonalBefattning = GeneratePersonalBefattning()
            });
        }

        return personal;
    }

    private static byte GeneratePersonalBefattning()
    {
        Random r = new();
        var randomValue = r.Next(1, 101);
        return randomValue <= 80 ? (byte)1 : randomValue <= 95 ? (byte)2 : (byte)3;
    }

}
