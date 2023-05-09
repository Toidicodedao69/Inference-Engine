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
        private string _left, _right;
        private int _count;
        
        public Sentence(string sentence)
        {
            _symbols = new List<string>();
            _connectives = new List<string>();
            _sentence = sentence;
            _count = 0;
            Initialize();
        }

        // Extract symbols and connectives from the sentence
        // This method is called inside the constructor
        public void Initialize()
        {
            if (_sentence.Length <= 2) // A symbol
            {
                _symbols.Add(_sentence);
            }
            else
            {
                string[] side = Regex.Split(_sentence, @"[=>]+");       // Split left side and right side of the sentence
                //foreach (string s in side)
                //{
                //    Console.WriteLine(s);
                //}
                _left = side[0];
                _right = side[1];

                foreach (string s in _left.Split("&"))
                {
                    _symbols.Add(s);
                    _count++;
                }
                
                _symbols.Add(_right);
            }
        }
        
        public void Construct()
        {

        }
        public List<string> getSymbols {  get { return _symbols; } }   
        public string getSentence { get { return _sentence; } }
        public List<string> getConnective { get { return _connectives; } }
        public int Count { 
            get { return _count;}
            set { _count = value; }
        }
        public string getLeft { get { return _left; } }
        public string getRight { get { return _right; } }   
    }
}
