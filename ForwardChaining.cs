using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class ForwardChaining : SearchAlgorithm
    {
        private Queue<string> _symbols;
        private KnowledgeBase _KB;
        public ForwardChaining(KnowledgeBase kB) 
        {
            _KB = kB;
            _symbols = new Queue<string>();
            foreach(Sentence s in _KB.getSentences)
            {
                if (s.getSentence.Length <= 2)
                {
                    _symbols.Enqueue(s.getSentence);
                }
            }
        }
        public override void Entails()
        {
            string symbol;
            List<string> solution = new List<string>();
            while (_symbols.Count > 0)
            {
                symbol = _symbols.Dequeue();
                solution.Add(symbol);

                if (solution.Contains(_KB.getQuery.getSentence))
                {
                    break;
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

            if (solution.Contains(_KB.getQuery.getSentence))
            {
                Console.Write("YES: ");

                foreach (string s in solution)
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
