using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class Eval
    {
        //evaluation per 1 symbol (as in [R, True] or [R, False])
        private string _symbol;
        private bool _boolValue;
        public Eval(string symbol, bool boolValue)
        {
            _symbol = symbol;
            _boolValue = boolValue;
        }

        public string Symbol { get { return _symbol; } }
        public bool BoolValue { get { return _boolValue; } }
    }
}
