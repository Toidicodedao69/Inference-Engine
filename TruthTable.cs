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
            //string symbols3 = "";
            //foreach (var m in model.ModelSet)
            //{
            //    symbols3 += "[" + m.Key + "-" + m.Value + "] ";
            //}
            //Console.WriteLine("Model symbols and value: " + symbols3);
            if (symbols.Count == 0)
            {
                //if PL - TRUE? (KB, model) then return PL - TRUE? (α, model)
                //else return true // when KB is false, always return true
                //_count++;
                //Console.WriteLine("\nChecking if model is true with KB\n");
                //Console.WriteLine("Count: " + _count);
                if (model.PL_True(KB))
                {
                   // Console.WriteLine("\nChecking if model is true with Query: {0}\n", query.getSentence);
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
                //Console.WriteLine("True branch executed");
                //foreach (var i in modelT.ModelSet)
                //{
                //    Console.WriteLine(i.Key + " " + i.Value);
                //}
                bool modelFB = CheckAll(KB, query, rest, modelF.Extend(first, false));
                //Console.WriteLine("False branch executed");
                //foreach (var i in modelF.ModelSet)
                //{
                //    Console.WriteLine(i.Key + " " + i.Value);
                //}

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
