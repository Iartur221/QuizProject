using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuizRozwiazywanie
{
    //Klasa modelu widoku udostępniąjąca widokowi odpowiednie własności 
    public class BindSolve : ChangeProperty
    {
        private Quiz viewQuiz;
        
        public Dictionary<char, string> AnswerList {
            get
            {
                return viewQuiz.getCurrentQuestions();
            }
        }
        public string question
        {
            get { return viewQuiz.getCurrentContent(); }
        }
        public void next()
        {
            viewQuiz.NextQuestion();
        }
        public BindSolve copy()
        {
            return this;
        }
        public string name()
        {
            return viewQuiz.Name;
        }

        public BindSolve()
        {
            viewQuiz = new Quiz();
        }
        public BindSolve(string name, string path)
        {
            viewQuiz = new Quiz(name);
            viewQuiz.loadFromFile(path);
        }
        

    }
}
