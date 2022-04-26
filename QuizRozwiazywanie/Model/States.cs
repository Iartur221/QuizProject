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
        public static string Change(string con, string pts)
        {
            if (con != "Next")
                return con = "Next";
            return con = pts;
        }

        public static int CountPoints(bool a, bool b, bool c, bool d, int points, BindSolve quiz)
        {
            int copy = points;
            if (a == true)
            {
                if (quiz.correct('a') == true) copy++;
                else return points;
            }
            if (b == true)
            {
                if (quiz.correct('b') == true) copy++;
                else return points;
            }
            if (c == true)
            {
                if (quiz.correct('c') == true) copy++;
                else return points;
            }
            if (d == true)
            {
                if (quiz.correct('d') == true) copy++;
                else return points;
            }
            return copy;
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
        public static BindSolve Changestartbutton(string content, BindSolve selected)
        {
            if (content == "Start")
            {
                return selected;
            }
            else
            {
                return selected = new BindSolve();
            }
        }
        public static int calculatemax(int maxpts, BindSolve current)
        {
            return maxpts = current.questionamount();
        }

        public static BindSolve ValueOf(BindSolve content)
        {
            return content.copy();
        }

        

    }
    #endregion
}
