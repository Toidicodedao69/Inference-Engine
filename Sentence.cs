using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InferenceEngine
{
    // A sentence contains symbols and logical connectives
    // A single symbol is also a sentence
    public class Sentence
    {
        private List<string> _symbols, _connectives;
        private string _sentence;
        
        public Sentence(string sentence)
        {
            _symbols = new List<string>();
            _connectives = new List<string>();
            _sentence = sentence;
            Initialize();
        }

        // Extract symbols and connectives from the sentence
        // This method is called inside the constructor
        public void Initialize()
        {
            if (_sentence.Length == 1) // A symbol
            {
                _symbols.Add(_sentence);
            }
            else
            {
                foreach (string s in Regex.Split(_sentence, @"&*[=>]+"))
                {
                    _symbols.Add(s);                 
                }
            }
        }
        public List<string> getSymbols {  get { return _symbols; } }   
        public string getSentence { get { return _sentence; } }
        public List<string> getConnective { get { return _connectives; } }
    }
}
