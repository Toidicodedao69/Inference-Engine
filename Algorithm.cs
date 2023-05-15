using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public abstract class Algorithm
    {
        public abstract bool Entails();

        public abstract void PrintResult();
    }
}
