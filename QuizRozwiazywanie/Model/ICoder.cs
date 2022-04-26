using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizRozwiazywanie
{
    public interface ICoder<T>
    {
        T Encode(T data);
        T Decode(T data);
    }
}
