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
        private int _count;
        public TruthTable(KnowledgeBase KB)
        {
            _KB = KB;
            _numOfModels = 0;
            _count = 0;
        }

        public override void Entails()
        {
            CheckAll(_KB, _KB.Query, _KB.getSymbols, new Model()); //first check all with empty model set
        }

        private void CheckAll(KnowledgeBase KB, Sentence query, List<string> symbols, Model model)
        {
            string symbols3 = "";
            foreach (var m in model.ModelSet)
            {
                symbols3 += "[" + m.Key + "-" + m.Value + "] ";
            }
            Console.WriteLine("Model symbols and value: " + symbols3);
            if (symbols.Count == 0)
            {
                //if PL - TRUE? (KB, model) then return PL - TRUE? (α, model)
                //else return true // when KB is false, always return true
                _count++;
                Console.WriteLine("\nChecking if model is true with KB\n");
                Console.WriteLine("Count: " + _count);
                if (model.PL_True(KB))
                {
                    Console.WriteLine("\nChecking if model is true with Query: {0}\n", query.getSentence);
                    if (model.PL_True(query))
                    {
                        _numOfModels++;
                        Console.WriteLine("Plus 1.");
                    }
                    //return model.PL_True(query);
                    
                }
                //else
                //{
                    //Console.WriteLine("KB not true lol.");
                    //_numOfModels++; //?
                    //return true;
                //}
            }
            else
            {
                //string symbols1 = "";
                //foreach (string symbol in symbols)
                //{
                //    symbols1 += symbol + " ";
                //}
                //Console.WriteLine("Check All symbols: " + symbols1);

                string first = symbols.First(); //take out the first symbol
                symbols.RemoveAt(0);
                List<string> rest = new List<string>();
                rest.AddRange(symbols); //store the rest of the symbols

                //string symbols2 = "";
                //foreach (string symbol in symbols)
                //{
                //    symbols2 += symbol + " ";
                //}
                //Console.WriteLine("Rest symbols: " + symbols2);

                //create new model for each branch 
                Model modelT = new Model();
                Model modelF = new Model();

                modelT = modelF = model;

                //assign the new model to be the input model value plus the new symbol with either True/False bool value
                modelT.Extend(first, true);
                Console.WriteLine("True branch executed");
                CheckAll(KB, query, rest, modelT);
                modelF.Extend(first, false);
                Console.WriteLine("False branch executed");
                CheckAll(KB, query, rest, modelF);

                //recursively call CheckAll
                //recursively call CheckAll
                //CheckAll(KB, query, rest, modelT);
                //CheckAll(KB, query, rest, modelF);
            }
        }

        public override void PrintResult()
        {
            if (_numOfModels > 0)
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
