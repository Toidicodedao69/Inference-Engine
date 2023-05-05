using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InferenceEngine
{
    // This class is the Knowledge Base
    public class KnowledgeBase
    {
        private List<Sentence> _sentences;  // List of all senteces/ clauses
        private List<string> _symbols; // List of all DISTINCT symbols
        public KnowledgeBase() 
        {
            _symbols = new List<string>();
            _sentences = new List<Sentence>();
        }
        public void ReadFile(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.Open);
            StreamReader reader = new StreamReader(stream);

            try
            {
                reader.ReadLine();  // "TELL" keyword
                string kB = reader.ReadLine(); // This line contains all sentences/clauses

                reader.ReadLine();  // "ASK" keyword
                string query = reader.ReadLine(); // The query 

                kB = Regex.Replace(kB, @"\s+", ""); // Remove all whitespaces

                string[] clauses = kB.Split(";");   // Get each clause

                for (int i = 0; i < clauses.Length; i++)
                {
                    Sentence temp = new Sentence(clauses[i]);
                    _sentences.Add(temp);

                    // Take symbol(s) from each sentenct and append to the _symbols list
                    foreach (string s in temp.getSymbols)
                    {
                        _symbols.Add(s);
                    }
                }

                _symbols = _symbols.Distinct().ToList(); // Remove all duplicated symbols

                // Check the symbols list
                foreach(string s in _symbols)
                {
                    Console.WriteLine(s);
                }

                // Check the sentence list
                foreach (Sentence s in _sentences)
                {
                    Console.WriteLine(s.getSentence);       
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Environment.Exit(1);
            }
        }
        public List<Sentence> getSentences { get { return _sentences; } } 
        public List<string> getSymbols { get { return _symbols; } }
    }
}
