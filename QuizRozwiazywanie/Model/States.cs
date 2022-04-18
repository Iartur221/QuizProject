using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizRozwiazywanie
{
    public static class States
    {
        public static void NextQuestion(BindSolve state)
        {
            state.next();
        }
    }
}
