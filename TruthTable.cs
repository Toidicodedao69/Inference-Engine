using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class TruthTable : Algorithm
    {
        private List<string> _symbols;
        private KnowledgeBase _KB;
        private int _numOfModels;
        private int _count;
        private List<string> _rest;
        public TruthTable(KnowledgeBase KB)
        {
            _KB = KB;
            _symbols = KB.getSymbols;
            _numOfModels = 0;
            _count = 0;
            _rest = new List<string>();
        }

        public override bool Entails()
        {
            return CheckAll(_KB, _KB.Query, _symbols, new Model()); //first check all with empty model set
        }

        private bool CheckAll(KnowledgeBase KB, Sentence query, List<string> symbols, Model model)
        {
            if (_symbols.Count == 0)
            {
                //if PL - TRUE? (KB, model) then return PL - TRUE? (α, model)
                //else return true // when KB is false, always return true
                if (model.PL_True(KB))
                {
                    if (model.PL_True(KB.Query)) 
                    {
                        _numOfModels++;
                        return true;
                    }
                    return false; 
                }
                else // when KB is false, always return true as (KB => a)
                {
                    return true;
                }
            }
            else
            {
                string first = _symbols.First(); //take out the first symbol\
                _symbols.Remove(first);
                _rest.AddRange(_symbols); //store the rest of the symbols

                //create new model for each branch 
                Model modelT = new Model(); 
                Model modelF = new Model();

                //assign the new model to be the input model value plus the new symbol with either True/False bool value
                modelT.ModelSet.AddRange(model.ModelSet);
                modelT.Extend(first, true);

                Console.WriteLine("Count: {0}", _count);
                Console.WriteLine("True branch: ");

                foreach (Eval e in modelT.ModelSet)
                {
                    Console.WriteLine("[{0}, {1}]", e.Symbol, e.BoolValue);
                }

                modelF.ModelSet.AddRange(model.ModelSet); 
                modelF.Extend(first, false);

                Console.WriteLine("False branch: ");
                foreach (Eval e in modelF.ModelSet)
                {
                    Console.WriteLine("[{0}, {1}]", e.Symbol, e.BoolValue);
                }

                Console.WriteLine("Original model: ");
                foreach (Eval e in model.ModelSet)
                {
                    Console.WriteLine("[{0}, {1}]", e.Symbol, e.BoolValue);
                }

                _count++;

                //recursively call CheckAll
                return CheckAll(KB, KB.Query, _rest, modelT) && CheckAll(KB, KB.Query, _rest, modelF);
            }
        }

        public override void PrintResult()
        {
            if (Entails())
            {
                Console.WriteLine("YES: {0}", _numOfModels);
            }
            else
            {
                Console.WriteLine("NO");
            }
        }
    }
}
