using System.Text.RegularExpressions;

namespace InferenceEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            KnowledgeBase kB = new KnowledgeBase();
            kB.ReadFile(args[0]);
            kB.ReadFile("test_genericKB.txt");
            //kB.ReadFile("small_test.txt");

            //Algorithm FC = new ForwardChaining(kB);
            //FC.Entails();
            //FC.PrintResult();

            Algorithm TT = new TruthTable(kB);
            TT.Entails();
            TT.PrintResult();
        }

    }
}
