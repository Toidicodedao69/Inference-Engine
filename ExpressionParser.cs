﻿using System;
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

        public string Parse(string expression)             //infix to postfix parser
        {
            string result = "";
            Regex pattern = new Regex("[a-zA-Z0-9]");

            for (int i = 0; i < expression.Length; i++) //acquiring the symbol stack
            {
                string parsing;
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
                    result += parsing;
                }
                else //not alphanumeric => connective or maybe brackets () //when to push onto the stack?
                {
                    if (expression[i].ToString() == "(") //if it's an open bracket, push to stack
                    {
                        parsing = expression[i].ToString();
                        _parseStack.Push(parsing);
                    }
                    else if (expression[i].ToString() == ")")
                    {
                        while (_parseStack.Peek() != "(")
                        {
                            result += _parseStack.Pop();  //pop until the open bracket is met
                        }
                        _parseStack.Pop();
                    }
                    else //connectives
                    {
                        parsing = expression[i].ToString();
                        if (_connectives.ContainsKey(expression[i].ToString())) //1 character connective - &, ~
                        {
                            parsing = expression[i].ToString();
                        }
                        else//2 character connective or 3 char connective
                        {
                            if (i <= expression.Length - 2)
                            {
                                if (!pattern.IsMatch(expression[i + 1].ToString()))
                                {
                                    parsing += expression[i + 1].ToString();
                                    if (_connectives.ContainsKey(parsing)) //2 char connective
                                    {
                                        i = i + 1;
                                    }
                                    else
                                    {
                                        if (i <= expression.Length - 3)
                                        {
                                            if (!pattern.IsMatch(expression[i + 2].ToString())) //3 char connective
                                            {
                                                parsing += expression[i + 2].ToString();
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
                        //operator have been stored in parsing
                        while (_parseStack.Count > 0 && _parseStack.Peek() != "(" && _connectives[_parseStack.Peek()] >= _connectives[parsing])
                        {
                            result += _parseStack.Pop();
                        }
                        _parseStack.Push(parsing);

                    }
                }
                parsing = ""; //reset the string
            }

            while (_parseStack.Count != 0)
            {
                result += _parseStack.Pop();
            }

            //Console.WriteLine(result);
            return result;
        }
    }
}
