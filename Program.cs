using System.Text.RegularExpressions;

namespace InferenceEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            KnowledgeBase kB = new KnowledgeBase();
            kB.ReadFile(args[0]);

            SearchAlgorithm FC = new ForwardChaining(kB);
            FC.Entails();
        }

    }
}
