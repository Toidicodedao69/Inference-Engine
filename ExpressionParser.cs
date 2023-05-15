using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace InferenceEngine
{
    public class ExpressionParser
    {
        private Stack<string> _parseStack;
        private Dictionary<string, int> _connectives; //Connective(string connective, int priority <precedence>)

        public ExpressionParser()
        {
            _parseStack = new Stack<string>();
            _connectives = new Dictionary<string, int>() { { "&", 3 }, { "=>", 2 }, { "~", 4 }, { "||", 3 }, { "<=>", 2 } };
        }

        public void Parse(string expression)
        {
            //should this function return the bool value of the expression straght away

            //parsing an expression, how
            //infix to postfix parser
            string parsing;
            Regex pattern = new Regex("[a-zA-Z0-9]");

            for (int i = 0; i < expression.Length; i++) //acquiring the symbol stack
            {
                if (pattern.IsMatch(expression[i].ToString()))
                {
                    parsing = expression[i].ToString(); //1 char symbol
                    if (i <= expression.Length - 2)
                    {
                        if (pattern.IsMatch(expression[i + 1].ToString())) //2nd char of the symbol
                        {
                            parsing += expression[i + 1].ToString(); //symbol with number
                            i = i + 1;
                        }
                    }
                    _parseStack.Push(parsing);
                }
                else //not alphanumeric => connective
                {
                    parsing = expression[i].ToString();
                    if (_connectives.ContainsKey(expression[i].ToString())) //1 character connective
                    { 
                        _parseStack.Push(parsing);
                    }
                    else //2 character connective or 3 char connective
                    {
                        if (i <= expression.Length - 2)
                        {
                            if (!pattern.IsMatch(expression[i + 1].ToString()))
                            {
                                parsing += expression[i + 1].ToString();
                                if (_connectives.ContainsKey(parsing)) //2 char connective
                                {
                                    i = i + 1;
                                    _parseStack.Push(parsing);
                                }
                                else
                                {
                                    if (i <= expression.Length - 1)
                                    {
                                        if (!pattern.IsMatch(expression[i + 2].ToString())) //3 char connective
                                        {
                                            parsing += expression[i + 2].ToString();
                                            if (_connectives.ContainsKey(parsing))
                                            {
                                                i = i + 2;
                                                _parseStack.Push(parsing);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                parsing = "";
            }

            //evaluation with the stack?

            while (_parseStack.Count > 0)
            {
                string current = _parseStack.Pop();
                Console.WriteLine(current);
                if (!_connectives.ContainsKey(current)) //not a connective
                {

                }

            }
        }
    }

       // private bool IsConnective(string connective)
        //{
            
       // }
}
