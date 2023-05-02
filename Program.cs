using System.Text.RegularExpressions;

namespace InferenceEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ReadFile(args[0]);
        }
        public static void ReadFile(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.Open);
            StreamReader reader = new StreamReader(stream);

            try
            {
                reader.ReadLine();  // "TELL" keyword
                string kB = reader.ReadLine(); // Knowledge base

                reader.ReadLine();  // "ASK" keyword
                string query = reader.ReadLine(); // The query 

                kB = Regex.Replace(kB, @"\s+", ""); // Remove all whitespaces
                string[] clauses = kB.Split(";");   // Get each clause
                
                for (int i = 0; i < clauses.Length; i++)
                {
                    Console.WriteLine(clauses[i]);
                }
                Console.WriteLine("Query: " + query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Environment.Exit(1);
            }
        }
    }
}
