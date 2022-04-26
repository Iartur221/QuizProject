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
        public bool correct(char x)
        {
            return viewQuiz.CheckAnswer(x);
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
        public int questionamount()
        {
            List<Question> a = viewQuiz.GetQuestions();
            int amount = 0;
            foreach (Question q in a)
            {
                amount += q.getCorrectsAsString().Length;
            }
            return amount;           
        }
        public BindSolve()
        {
            viewQuiz = new Quiz();
        }
        public BindSolve(string name, string path)
        {
            viewQuiz = new Quiz(name);
            viewQuiz.loadFromFile(path);
            viewQuiz.loadFromFile(path, new Base64Coder());
        }
        

    }
}
