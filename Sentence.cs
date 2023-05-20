using System;
using System.Collections.Generic;
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
        private Dictionary<string, int> _connectives; //Connective(string connective, int priority <precedence>)

        public Sentence(string sentence)
        {
            _symbols = new List<string>();
            _leftSymbols = new List<string>();
            _sentence = sentence;
            _count = 0;
            _connectives = new Dictionary<string, int>() { { "&", 3 }, { "=>", 2 }, { "~", 4 }, { "||", 3 }, { "<=>", 2 } };
            Initialize();
        }

        // Extract symbols and connectives from the sentence
        // This method is called inside the constructor
        public void Initialize()
        {
            //general knowledge
            string result = "";
            Regex pattern = new Regex("[a-zA-Z0-9]");
            Stack<string> connectives = new Stack<string>();

            for (int i = 0; i < _sentence.Length; i++) 
            {
                string parsing;
                if (pattern.IsMatch(_sentence[i].ToString()))
                {
                    parsing = _sentence[i].ToString(); //1 char symbol
                    if (i <= _sentence.Length - 2)
                    {
                        if (pattern.IsMatch(_sentence[i + 1].ToString())) //2nd char of the symbol
                        {
                            parsing += _sentence[i + 1].ToString(); //symbol with number
                            i = i + 1;
                        }
                    }
                    //symbol
                    _symbols.Add(parsing);
                    _leftSymbols.Add(parsing);
                }
                else 
                {
                    if (_sentence[i].ToString() == "(") //if it's an open bracket, push to stack
                    {
                        parsing = _sentence[i].ToString();
                    }
                    else if (_sentence[i].ToString() == ")")
                    {
                        parsing = _sentence[i].ToString();
                    }
                    else //connectives
                    {
                        parsing = _sentence[i].ToString();
                        if (_connectives.ContainsKey(_sentence[i].ToString())) //1 character connective - &, ~
                        {
                            parsing = _sentence[i].ToString();
                        }
                        else//2 character connective or 3 char connective
                        {
                            if (i <= _sentence.Length - 2)
                            {
                                if (!pattern.IsMatch(_sentence[i + 1].ToString()))
                                {
                                    parsing += _sentence[i + 1].ToString();
                                    if (_connectives.ContainsKey(parsing)) //2 char connective - =>, ||
                                    {
                                        i = i + 1;
                                    }
                                    else
                                    {
                                        if (i <= _sentence.Length - 3)
                                        {
                                            if (!pattern.IsMatch(_sentence[i + 2].ToString())) //3 char connective
                                            {
                                                parsing += _sentence[i + 2].ToString();
                                                if (_connectives.ContainsKey(parsing))
                                                {
                                                    i = i + 2;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                result = parsing;
                parsing = ""; //reset the string
            }

            // Remove the conclusion symbol from the returned list
            _leftSymbols.RemoveAt(_leftSymbols.Count - 1);

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

        // getleft returns the List of all symbols on the left before "=>" connective, which is P1, P2, ...,Pn
        // getRight returns the conclusion symbol on the right after "=>" connective, which is Q
        public List<string> getLeft()
        {
            return _leftSymbols;
        }
        public string getRight()
        { 
            return _symbols.Last();
        }   
    }
}
