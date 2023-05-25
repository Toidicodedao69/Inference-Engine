using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class BackwardChaining : Algorithm
    {
        private Queue<string> _subgoals, _closed_subgoals;
        private List<string> _facts; // true symbols
        private KnowledgeBase _KB;
        private bool _entails;

        public BackwardChaining(KnowledgeBase kB) 
        {
            _KB = kB;

            _subgoals = new Queue<string>();
            _closed_subgoals = new Queue<string>();
            _facts = new List<string>();
            _entails = true;

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
            _subgoals.Enqueue(_KB.Query.getSentence);

            string symbol;

            while (_subgoals.Count > 0)
            {
                 symbol = _subgoals.Dequeue();

                _closed_subgoals.Enqueue(symbol);

                // If the symbol is not given true in KB
                if (!_facts.Contains(symbol))
                {
                    List<Sentence> sub_goal_sentences = new List<Sentence>();

                    foreach (Sentence s in _KB.getSentences)
                    {
                        if (s.getConclusion() == symbol)
                        {
                            // Get all the sentences that concluding symbol
                            sub_goal_sentences.Add(s);
                        }
                    }

                    if (sub_goal_sentences.Count == 0)
                    {
                        _entails = false;
                        break;
                    }
                    else
                    {
                        foreach (Sentence s in sub_goal_sentences)
                        {
                            foreach (string sym in s.getPremises())
                            {
                                if (!_closed_subgoals.Contains(sym))
                                {
                                    // Add new sub goals which do not exist in _closed_subgoals
                                    _subgoals.Enqueue(sym);
                                }
                            }
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
