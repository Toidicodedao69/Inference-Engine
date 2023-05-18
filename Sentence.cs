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
        private List<string> _symbols;
        private string _sentence;
        private string _left, _right;
        private int _count;
        private bool _atomic;
        private Dictionary<string, int> _connectives; //Connective(string connective, int priority <precedence>)

        public Sentence(string sentence)
        {
            _symbols = new List<string>();
            _sentence = sentence;
            _left = _right = "";
            _count = 0;
            _atomic = false;
            _connectives = new Dictionary<string, int>() { { "&", 3 }, { "=>", 2 }, { "~", 4 }, { "||", 3 }, { "<=>", 2 } };
            Initialize();
        }

        // Extract symbols and connectives from the sentence
        // This method is called inside the constructor
        public void Initialize()
        {
            ////horn - remember to fix
            //if (_sentence.Length <= 2) // A symbol - later we will have <=> which will have the length of 3 lol
            //{
            //    _symbols.Add(_sentence);
            //    _atomic = true;

            //}
            //else
            //{
            //    string[] side = Regex.Split(_sentence, @"[=>]+");       // Split left side and right side of the sentence

            //    _left = side[0];
            //    _right = side[1];

            //    foreach (string s in _left.Split("&"))
            //    {
            //        _symbols.Add(s);
            //        _count++;
            //    }
            //    _symbols.Add(_right);

            //}

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
                    _symbols.Add(parsing); //symbol
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


        }
        public bool IsAtomic { get { return _atomic; } }

        public List<string> getSymbols {  get { return _symbols; } }   
        public string getSentence { get { return _sentence; } }
        public int Count { 
            get { return _count;}
            set { _count = value; }
        }
        public string getLeft { get { return _left; } }
        public string getRight { get { return _right; } }   
    }
}
