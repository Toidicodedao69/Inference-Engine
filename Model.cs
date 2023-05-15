using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class Model
    {
        private List<Eval> _evals; //set of boolean values of symbols evaluated by truth table

        public Model()
        {
            _evals = new List<Eval>();
        }

        public List<Eval> ModelSet { get { return _evals; } set { _evals = value; } }

        public void Extend(string symbol, bool boolValue) //extend the current model set 
        {
            Eval eval = new Eval(symbol, boolValue);
            _evals.Add(eval);
        }

        public bool PL_True(KnowledgeBase KB)
        {
            bool final = true;
            foreach (Sentence s in KB.getSentences)
            {
                final = final && PL_True(s);
            }
            return final;
        }

        public bool PL_True(Sentence sentence)
        {
            bool final = true;
            //check for an atomic (simple) sentence - base case
            if (sentence.IsAtomic)
            {
                foreach (Eval e in _evals)
                {
                    if (sentence.getSymbols[0] == e.Symbol)
                    {
                        final = e.BoolValue;
                    }
                }
                return final;
            }
            //else //for complex sentences like p2=> p3 or b&e => f // guess the best solution is to implement a Parser
            //{
            //    if (sentence.Connective.Count == 1 && sentence.Connective[0] == "=>") //only =>
            //    {
            //        final = !PL_True(new Sentence(sentence.getLeft)) || PL_True(new Sentence(sentence.getRight));
            //    }

            //    if (sentence.Connective.Count > 1)
            //    {
            //        bool logic = true;
            //        Sentence leftSentence = new Sentence(sentence.getLeft); //left sentence with &
            //        Sentence rightSentence = new Sentence(sentence.getRight);

            //        if (sentence.getLeft.Contains("&"))
            //        {
            //            foreach (string s in sentence.getLeft.Split("&"))
            //            {
            //                logic = logic && !PL_True(new Sentence(s));
            //            }
            //        }
            //        final = !logic || PL_True(new Sentence(sentence.getRight));
            //    }
            //    return final;
            //}
            return final;
        }

        //parser?
    }
}
