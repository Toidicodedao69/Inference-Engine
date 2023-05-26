using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class BackwardChaining : Algorithm
    {
        private Queue<string> _closed_subgoals;
        private List<string> _facts; // true symbols
        private KnowledgeBase _KB;
        private bool _entails;

        public BackwardChaining(KnowledgeBase kB) 
        {
            _KB = kB;

            _closed_subgoals = new Queue<string>();
            _facts = new List<string>();
            _entails = false;

            foreach (Sentence s in _KB.getSentences)
            {
                if (s.Count == 0)
                {
                    _facts.Add(s.getSentence);
                }
            }
        }

        public override void Entails()
        {
            // Start from the query and work backwards
            _entails = TruthValue(_KB.Query.getSentence);
        }
        public bool TruthValue(string symbol)
        {
            _closed_subgoals.Enqueue(symbol);

            if (_facts.Contains(symbol))
            {
                return true;
            }

            List<Sentence> sub_goal_sentences = new List<Sentence>();

            foreach (Sentence s in _KB.getSentences)
            {
                if (s.getConclusion() == symbol)
                {
                    // Get all the sentences that concluding symbol
                    sub_goal_sentences.Add(s);
                }
            }

            bool truthvalue = false;

            // Examine the truth value of each symbol in each sub goal sentence
            foreach (Sentence s in sub_goal_sentences)
            {
                foreach (string sym in s.getPremises())
                {
                    if (_closed_subgoals.Contains(sym))
                    {
                        break;
                    }
                    else
                    {
                        truthvalue = TruthValue(sym);
                    }
                }
            }

            // If the symbol is true, add it the _facts list
            if (truthvalue)
            {
                _facts.Add(symbol);
            }
            return truthvalue;
        }
        public override void PrintResult()
        {
            if (_entails)
            {
                Console.Write("YES: ");

                foreach (string s in _closed_subgoals.Reverse())
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
