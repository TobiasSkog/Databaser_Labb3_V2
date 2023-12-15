namespace Databaser_Labb3_V2.Application.DataGenerators;

public class NameLists
{
    private static readonly Random random = new();
    private static readonly List<string> LastNames = ["Andersson", "Johansson", "Karlsson", "Nilsson", "Eriksson", "Larsson", "Olsson", "Persson", "Svensson", "Gustafsson", "Pettersson", "Jonsson", "Jansson", "Hansson", "Bengtsson", "Jönsson", "Lindberg", "Jakobsson", "Magnusson", "Lindström", "Olofsson", "Lindqvist", "Lindgren", "Berg", "Axelsson", "Bergström", "Lundberg", "Lind", "Lundgren", "Lundqvist", "Mattsson", "Berglund", "Fredriksson", "Sandberg", "Henriksson", "Ali", "Forsberg", "Mohamed", "Sjöberg", "Wallin", "Engström", "Eklund", "Danielsson", "Lundin", "Håkansson", "Björk", "Bergman", "Gunnarsson", "Wikström", "Holm", "Samuelsson", "Isaksson", "Fransson", "Bergqvist", "Nyström", "Holmberg", "Arvidsson", "Löfgren", "Söderberg", "Nyberg", "Ahmed", "Blomqvist", "Classon", "Nordström", "Hassan", "Mårtensson", "Lundström", "Viklund", "Björklund", "Eliasson", "Berggren", "Pålsson", "Sandström", "Nordin", "Lund", "Falk", "Ström", "Åberg", "Ekström", "Hermansson", "Holmgren", "Dahlberg", "Hellström", "Hedlund", "Sundberg", "Sjögren", "Ek", "Blom", "Abrahamsson", "Öberg", "Martinsson", "Andreasson", "Strömberg", "Månsson", "Hansen", "Åkesson", "Dahl", "Lindholm", "Norberg", "Holmqvist"];
    private static readonly List<string> MaleFirstNames = ["Karl", "Erik", "Lars", "Anders", "Per", "Mikael", "Johan", "Olof", "Nils", "Jan", "Gustav", "Hans", "Peter", "Lennart", "Fredrik", "Gunnar", "Thomas", "Sven", "Daniel", "Alexander", "Bengt", "Bo", "Oskar", "Göran", "Andreas", "Åke", "Christer", "Stefan", "Magnus", "Martin", "Mohamed", "John", "Mattias", "Mats", "Henrik", "Jonas", "Ulf", "Leif", "Björn", "Axel", "Christian", "Robert", "David", "Viktor", "Marcus", "Emil", "Niklas", "Bertil", "Arne", "Patrik", "Ingemar", "Håkan", "Christoffer", "Rickard", "Kjell", "William", "Joakim", "Stig", "Rolf", "Wilhelm", "Filip", "Tommy", "Roland", "Simon", "Sebastian", "Claes", "Anton", "Roger", "Kent", "Ingvar", "Elias", "Johannes", "Hugo", "Jakob", "Tobias", "Lucas", "Adam", "Jonathan", "Ove", "Emanuel", "Kenneth", "Robin", "Jörgen", "Ali", "Kurt", "Oliver", "Josef", "Rune", "Isak", "Georg", "Arvid", "Ludvig", "Gösta", "Linus", "Johnny", "Albin", "Olle", "Edvin", "Torbjörn", "Dan"];
    private static readonly List<string> FemaleFirstNames = ["Maria", "Elisabeth", "Anna", "Kristina", "Margareta", "Eva", "Linnéa", "Karin", "Birgitta", "Marie", "Ingrid", "Sofia", "Marianne", "Lena", "Sara", "Helena", "Kerstin", "Emma", "Johanna", "Katarina", "Viktoria", "Inger", "Cecilia", "Susanne", "Elin", "Monica", "Therese", "Jenny", "Hanna", "Louise", "Anita", "Carina", "Ann", "Irene", "Ida", "Linda", "Helen", "Gunilla", "Malin", "Viola", "Annika", "Ulla", "Elsa", "Matilda", "Josefin", "Ulrika", "Ingegerd", "Sofie", "Alice", "Anette", "Julia", "Astrid", "Caroline", "Anneli", "Emelie", "Kristin", "Karolina", "Amanda", "Lisa", "Barbro", "Åsa", "Camilla", "Madeleine", "Lovisa", "Erika", "Siv", "Maja", "Yvonne", "Charlotte", "Agneta", "Rut", "Sandra", "Britt", "Rebecca", "Isabelle", "Alexandra", "Frida", "Ellinor", "Gun", "Ebba", "Emilia", "Ellen", "Jessica", "Klara", "Olivia", "Berit", "Märta", "Charlotta", "Agnes", "Ann-Marie", "Inga", "Sonja", "Ella", "Ann-Christin", "Pia", "Felicia", "Maj", "Lilly", "Nathalie", "Mona"];
    public static string GetMaleFirstName() => MaleFirstNames[random.Next(MaleFirstNames.Count)];
    public static string GetFemaleFirstName() => FemaleFirstNames[random.Next(FemaleFirstNames.Count)];
    public static string GetLastName() => LastNames[random.Next(LastNames.Count)];
    private static readonly List<char> maleSsnChar = ['1', '3', '5', '7', '9'];
    private static readonly List<char> femaleSsnChar = ['0', '2', '4', '6', '8'];
    private static char GetGenderSsnChar(char gender) => gender switch
    {
        'M' => maleSsnChar[random.Next(maleSsnChar.Count)],
        _ => femaleSsnChar[random.Next(femaleSsnChar.Count)]
    };
    public static string GenerateRandomSsn(char gender)
    {
        int birthYear = random.Next(1960, 2009);
        int month = random.Next(1, 13);
        int day = random.Next(1, DateTime.DaysInMonth(birthYear, month));
        string ssn = $"{birthYear}{month:D2}{day:D2}";

        for (int i = 0; i < 4; i++)
        {
            ssn += random.Next(10).ToString();
        }

        if (gender == 'F' && (ssn[10] - '0') % 2 == 1)
        {
            ssn = ssn[..10] + GetGenderSsnChar(gender) + ssn[11..];
        }
        else if (gender == 'M' && (ssn[10] - '0') % 2 == 0 && ssn[10] - '0' != 0)
        {
            ssn = ssn[..10] + GetGenderSsnChar(gender) + ssn[11..];
        }

        return ssn;
    }
}
