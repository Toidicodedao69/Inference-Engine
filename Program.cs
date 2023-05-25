using System.Text.RegularExpressions;

namespace InferenceEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            KnowledgeBase kB = new KnowledgeBase();
            Algorithm? algorithm = null;

            // Read file
            kB.ReadFile(args[1]);

            // Read method
            switch (args[0])
            {
                case "fc":
                    algorithm = new ForwardChaining(kB);
                    break;
                case "bc":
                    algorithm = new BackwardChaining(kB);
                    break;
                case "tt":
                    algorithm = new TruthTable(kB);
                    break;
                default:
                    Console.WriteLine("Method not recognized");
                    break;
            }
            
            if (algorithm != null)
            {
                algorithm.Entails();
                algorithm.PrintResult();
            }

        }

    }
}
