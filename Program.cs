using Databaser_Labb3_V2.Application;

namespace Databaser_Labb3_V2;

internal class Program
{
    static void Main(string[] args)
    {
        App Labb3 = new();
        Labb3.Run().Wait();
    }
}
