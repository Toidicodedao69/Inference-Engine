using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InferenceEngine
{
    // A sentence contains symbols and logical connectives
    // A single symbol is also a sentence
    public class Sentence
    {
        private List<string> _symbols, _leftSymbols;
        private string _sentence;
        private int _count;

        public Sentence(string sentence)
        {
            _symbols = new List<string>();
            _leftSymbols = new List<string>();
            _sentence = sentence;
            _count = 0;
            Initialize();
        }

        // Extract symbols from the sentence
        // This method is called inside the constructor
        public void Initialize()
        {
            string parsing = "";
            Regex pattern = new Regex("[a-zA-Z0-9]");

            for (int i = 0; i < _sentence.Length; i++) 
            {
                if (pattern.IsMatch(_sentence[i].ToString()))
                {
                    parsing += _sentence[i].ToString();

                    // When it is at the end of sentence
                    if (i == _sentence.Length - 1)
                    {
                        _symbols.Add(parsing);
                        _leftSymbols.Add(parsing);
                    }
                }
                else
                {
                    // Pass in symbols when it meets special character
                    if (parsing != "")
                    {
                        _symbols.Add(parsing);
                        _leftSymbols.Add(parsing);

                        // Reset the parsing string
                        parsing = "";
                    }
                }
            }

            // Remove the conclusion symbol from the returned list
            _leftSymbols.RemoveAt(_leftSymbols.Count - 1);

            // Remove all duplicated symbols on the left side 
            // For example: "a&a&a => b"    -> The left symbol is only "a"    
            _leftSymbols = _leftSymbols.Distinct().ToList();

            // Initialize _count 
            _count = _leftSymbols.Count;
        }
        public List<string> getSymbols {  get { return _symbols; } }   
        public string getSentence { get { return _sentence; } }
        public int Count 
        { 
            get { return _count; }
            set { _count = value; }
        }

        // This 2 methods are used for Horn Knowledge Base which contains sentence in the following form: P1 & P2 & ... & Pn => Q

        // getPremises returns the List of all symbols on the left before "=>" connective, which is P1, P2, ...,Pn
        // getConclusion returns the conclusion symbol on the right after "=>" connective, which is Q
        public List<string> getPremises()
        {
            return _leftSymbols;
        }
        public string getConclusion()
        { 
            return _symbols.Last();
        }   
    }
}
