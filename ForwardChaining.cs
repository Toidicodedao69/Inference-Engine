using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class ForwardChaining : Algorithm
    {
        private Queue<string> _symbols;     // List of already true/inferred symbols
        private KnowledgeBase _KB;
        private List<String> _solution;     // List contains the solution path 
        private bool _entails;
        public ForwardChaining(KnowledgeBase kB)
        {
            _KB = kB;
            _symbols = new Queue<string>();
            _solution = new List<string>();
            _entails = false;

            // Get already true symbols in the kB
            foreach (Sentence s in _KB.getSentences)
            {
                if (s.getSymbols.Count < 2)
                {
                    _symbols.Enqueue(s.getSentence);
                }
            }
        }
        public override void Entails()
        {
            string symbol;
            while (_symbols.Count > 0)
            {
                symbol = _symbols.Dequeue();
                _solution.Add(symbol);

                foreach (Sentence s in _KB.getSentences)
                {
                    if (s.Count > 0 && s.getPremises().Contains(symbol))
                    {
                        s.Count--;

                        // If all the premises are satisfied
                        if (s.Count == 0)
                        {
                            // If the query is inferred
                            if (s.getConclusion() == _KB.Query.getSentence)
                            {
                                _solution.Add(s.getConclusion());
                                _entails = true;
                                break;
                            }
                            // Add the newly inferred symbol to the queue
                            _symbols.Enqueue(s.getConclusion());
                        }
                    }
                }
            }
        }

        public override void PrintResult()
        {
            if (_entails)
            {
                Console.Write("YES: ");

                foreach (string s in _solution.Distinct().ToList())
                {
                    Console.Write(s + " ");
                }

            }
            else
            {
                Console.WriteLine("NO");
            }
        }
    }
}