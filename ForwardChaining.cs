﻿using System;
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
        public ForwardChaining(KnowledgeBase kB)
        {
            _KB = kB;
            _symbols = new Queue<string>();

            // Get already true symbols in the kB
            foreach (Sentence s in _KB.getSentences)
            {
                if (s.getSentence.Length <= 2)
                {
                    _symbols.Enqueue(s.getSentence);
                }
            }
            _solution = new List<string>();
        }
        public override void Entails()
        {
            string symbol;
            while (_symbols.Count > 0)
            {
                symbol = _symbols.Dequeue();
                _solution.Add(symbol);

                if (_solution.Contains(_KB.Query.getSentence))
                {
                    break;
                }

                foreach (Sentence s in _KB.getSentences)
                {
                    if (s.Count > 0)
                    {
                        if (s.getLeft().Contains(symbol))
                        {
                            s.Count--;

                            if (s.Count == 0)
                            {
                                _symbols.Enqueue(s.getRight());
                            }
                        }
                    }
                }
            }
        }

        public override void PrintResult()
        {
            if (_solution.Contains(_KB.Query.getSentence))
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