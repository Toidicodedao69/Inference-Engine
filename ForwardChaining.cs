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
        private Queue<string> _symbols;
        private KnowledgeBase _KB;
        private List<String> _solution;
        public ForwardChaining(KnowledgeBase kB)
        {
            _KB = kB;
            _symbols = new Queue<string>();
            foreach (Sentence s in _KB.getSentences)
            {
                if (s.getSentence.Length <= 2)
                {
                    _symbols.Enqueue(s.getSentence);
                }
            }
            _solution = new List<string>();
        }
        public override bool Entails()
        {
            string symbol;
            while (_symbols.Count > 0)
            {
                symbol = _symbols.Dequeue();
                _solution.Add(symbol);

                if (_solution.Contains(_KB.Query.getSentence))
                {
                    return true;
                }

                foreach (Sentence s in _KB.getSentences)
                {
                    if (s.Count > 0)
                    {
                        if (s.getLeft.Contains(symbol))
                        {
                            s.Count--;
                            if (s.Count == 0)
                            {
                                _symbols.Enqueue(s.getRight);
                            }
                        }
                    }
                }
            }
            return false;
        }

        public override void PrintResult()
        {
            if (Entails())
            {
                Console.Write("YES: ");

                foreach (string s in _solution)
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