using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace QuizRozwiazywanie
{
    #region
    /// <summary>
    /// klasa funkcji pomocnicze dla viewmodelu
    /// </summary>
    
    public static class States
    {
        public static void NextQuestion(BindSolve state)
        {
            state.next();
        }

        //zmiana tekstu
        public static string StartOrMenu(string content)
        {
            if (content == "Start")
            {
                return content = "Stop";
            }
            return content = "Start";
        }
        public static string Change(string con)
        {
            if (con == "")
                return con = "Next";
            return con = "";
        }


        public static BindSolve Changestartbutton(string content, BindSolve selected, BindSolve current, Timer timer)
        {
            if (content == "Start")
            {
                current = selected;
                timer.Start();
                return current;
            }
            else
            {
                //zwroc pusty quiz
                timer.Stop();
                return current = new BindSolve();
            }
        }

        public static BindSolve ValueOf(BindSolve content)
        {
            return content.copy();
        }

        

    }
    #endregion
}
