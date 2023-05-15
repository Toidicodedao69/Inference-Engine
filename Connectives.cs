using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class Connectives
    {
        private Dictionary<string, int> _connectives; //Connective(string connective, int priority <precedence>)
        public Connectives()
        {
            _connectives = new Dictionary<string, int>();
        }
        
        //public Dictionary<string, int>

            //do i need this class tho, i can just include a dictionary in the expression parser?
    }
}
