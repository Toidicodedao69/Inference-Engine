﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class Model
    {
        //private List<Eval> _evals; //set of boolean values of symbols evaluated by truth table
        private Dictionary<string, bool> _evals;
        private Stack<bool> _operands;

        public Model()
        {
            //_evals = new List<Eval>();
            _evals = new Dictionary<string, bool>();
            _operands = new Stack<bool>();
        }

        public Dictionary<string, bool> ModelSet { get { return _evals; } set { _evals = value; } }

        public Model Extend(string symbol, bool boolValue) //extend the current model set 
        {
            _evals[symbol] = boolValue;
            return this;
        }

        public bool PL_True(KnowledgeBase KB)
        {
            bool final = true;
            bool result = false;
           // string final2 = "KB = True";
            foreach (Sentence s in KB.getSentences)
            {
                result = PL_True(s);
                final = final && result; //evaluate all KB sentences, if all true then true;
                //final2 += "&" + result.ToString();
            }
            //Console.WriteLine(final2);
            return final;
        }

        public bool PL_True(Sentence sentence)
        {
            ExpressionParser parser = new ExpressionParser();

            string postfix = parser.Parse(sentence.getSentence);
            //Console.WriteLine("Postfix:" + postfix);

            //string symbols1 = "";
            //foreach (var m in _evals)
            //{
            //    symbols1 += " [" + m.Key + ", " + m.Value + "]";
            //}

            //Console.WriteLine("Model symbols while evaluating PLTrue: " + symbols1);

            Regex pattern = new Regex("[a-zA-Z0-9]");
            Regex pattern2 = new Regex("[0-9]");

            bool operandA, operandB, smallSentenceE;

            string parsing = "";

            for (int i = 0; i < postfix.Length; i++)
            {
                if (pattern.IsMatch(postfix[i].ToString())) //extract symbol
                {
                    parsing = postfix[i].ToString(); 

                    // Keeps reading symbol until meets a special character
                    while (i + 1 < postfix.Length && pattern.IsMatch(postfix[i + 1].ToString()))
                    {
                        parsing += postfix[i + 1].ToString();
                        i = i + 1;
                    }

                    //compare to the existing evals in model
                    if (_evals.ContainsKey(parsing))
                    {
                        _operands.Push(_evals[parsing]); //push the scanned symbol onto the stack
                        //Console.WriteLine("Pushed [{0}, {1}]", parsing, _evals[parsing]);
                        //Console.WriteLine("Pushed: " + e.Symbol + " Value: " + e.BoolValue);
                        //break; //break out of the foreach loop
                        i++;
                    }
                }
                else //connectives
                {
                    parsing = postfix[i].ToString();
                    if (parser.IsConnective(postfix[i].ToString()))
                    {
                        parsing = postfix[i].ToString();
                    }

                    if (i <= postfix.Length - 2 && !pattern.IsMatch(postfix[i + 1].ToString()) && parser.IsConnective(parsing + postfix[i + 1].ToString())) //if 2 char connective
                    {
                        parsing += postfix[i + 1].ToString();
                        i = i + 1;  
                    }

                    if (i <= postfix.Length - 3 && !pattern.IsMatch(postfix[i + 2].ToString()) && parser.IsConnective(parsing + postfix[i + 1].ToString() + postfix[i + 2].ToString())) //3 char connective
                    {
                        parsing += postfix[i + 1].ToString() + postfix[i + 2].ToString();
                        i = i + 2;
                    }

                    if (parsing == "~")
                    {
                        if (_operands.Count > 0)
                        {
                            operandA = _operands.Pop();
                            operandA = !operandA;
                            _operands.Push(operandA);
                            //Console.WriteLine("Executed sentence: ~" + !operandA);
                            //Console.WriteLine("Pushed result: " + operandA);
                        }

                        if (i + 1 < postfix.Length && postfix[i + 1].ToString() == "~")
                        {
                            i = i + 1; //skips the symbol
                        }
                    }
                    else
                    {
                        operandA = _operands.Pop();
                       // Console.WriteLine("Popped: " + operandA);
                        operandB = _operands.Pop();
                        // Console.WriteLine("Popped: " + operandB);

                        //string smallSentence = operandB + parsing + operandA;
                        //Console.WriteLine(smallSentence);
                        //Console.WriteLine("Executed sentence: " + smallSentence);

                        smallSentenceE = Evaluate(operandB, operandA, parsing); //previous first

                        _operands.Push(smallSentenceE); //push the result of the evaluated sentence onto the stack
                        //Console.WriteLine("Pushed result: " + smallSentenceE);
                    }
                }
            }

                bool final = _operands.Pop();

                return final;
        }

        private bool Evaluate(bool a, bool b, string connective)
        {
            if (connective == "&")
            {
                return a && b;
            }
            else if (connective == "||")
            {
                return a || b;
            }
            else if (connective == "=>")
            {
                return Implication(a, b);
            }
            else if (connective == "<=>")
            {
                return (Implication(a, b)) && (Implication(b, a));
            }
            else
            {
                Console.WriteLine("Error.");
                throw new InvalidOperationException("Connective does not exists.");
            }
        }

        private bool Implication(bool a, bool b)
        {
            return !a || b;
        }
    }
}
