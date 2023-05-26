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
        private KnowledgeBase _KB;
        private int _numOfModels;
        //private int _count;
        private bool _entails;
        public TruthTable(KnowledgeBase KB)
        {
            _KB = KB;
            _numOfModels = 0;
            //_count = 0;
            _entails = false;
        }

        public override void Entails()
        {
            foreach(string symbol in _KB.Query.getSymbols)
            {
                // If query is not existed in the KB, print "NO" and terminate the program
                if (!_KB.getSymbols.Contains(symbol))
                {
                    return;
                }
            }
            _entails = CheckAll(_KB, _KB.Query, _KB.getSymbols, new Model());
        }

        private bool CheckAll(KnowledgeBase KB, Sentence query, List<string> symbols, Model model)
        {
            if (symbols.Count == 0)
            {
                if (model.PL_True(KB))
                {
                    if (model.PL_True(query))
                    {
                        _numOfModels++;
                        return true;
                    }
                    return false;
                }
                else return true; // when KB is false, always return true
            }
            else
            {
                string first = symbols.First(); //take out the first symbol
                List<string> rest = new List<string>();
                rest.AddRange(symbols); //store the rest of the symbols
                rest.Remove(rest.First());

                //create new model for each branch 
                Model modelT = new Model();
                Model modelF = new Model();

                modelT.ModelSet = modelF.ModelSet = model.ModelSet;

                //assign the new model to be the input model value plus the new symbol with either True/False bool value
                bool modelTB = CheckAll(KB, query, rest, modelT.Extend(first, true));

                bool modelFB = CheckAll(KB, query, rest, modelF.Extend(first, false));

                return modelTB && modelFB;
            }
        }

        public override void PrintResult()
        {
            if (_entails)
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
