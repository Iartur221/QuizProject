using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QuizRozwiazywanie
{
    public class ViewModelSolve : ChangeProperty
    {

        // lista wszystkich quizow
        private List<BindSolve> quizzes;
        private string[] lista;
        //Stan rozwiazywanego quizu
        //private BindSolve inWorkQuiz;
        //chosen quizstate
        private BindSolve selectedQuiz;
        //wyswietlane odpowiedzi
        private string answerA;
        private string answerB;
        private string answerC;
        private string answerD;
        //wyswietlane pytanie
        private string question;
        private RelayCommand nextQuestion;



        //polecenia dla trybu tworzenia

        public ViewModelSolve()
        {
            //wczytanie quizow z plikow do listy
            quizzes = new List<BindSolve>();
            lista = Directory.GetFiles("C:/Users/artur/source/repos/QuizRozwiazywanie/QuizRozwiazywanie/QuizFiles/");
            foreach (string var in lista)
            {
                quizzes.Add(new BindSolve(var,var));
            }

            try
            {
                //already a state
                //select the 1st
                selectedQuiz = quizzes[0];
            }
            catch (Exception)
            {
                //wywal okno z informacją o braku quizow
                //this.window.close()
            }
            currentQuiz = selectedQuiz;  
        }
        public BindSolve currentQuiz { get {
                return currentQuiz; }
            set
            {
                answerA = answerDict['a'];
                answerB = answerDict['b'];
                answerC = answerDict['c'];
                answerD = answerDict['d'];
                question = selectedQuiz.question;
            }
        }

        #region funkcje i zmienne do kontroli widoku
        public Dictionary<char, string> answerDict
        {
            get { return selectedQuiz.AnswerList; }
        }
        public string answerAout
        {  get { return answerA; }
           set { answerA = value;
                onPropertyChanged("answerAout");
            }
        }
        public string answerBout 
        { get { return answerB; }
          set { answerB = value;
                onPropertyChanged("answerBout");
            }
        }
        public string answerCout 
        { get { return answerC; }
          set { answerC = value;
                onPropertyChanged("answerCout");
            }
        }
        public string answerDout 
        { get { return answerD; }
          set { answerD = value;
                onPropertyChanged("answerDout");
            }
        }    
        public string questionout 
        { get { return question; }
          set { question = value;
                onPropertyChanged("questionout");
            }
        }

        //przyporzadkowanie funkcji do komendy
        public RelayCommand Next_Click
        {
            get
            {
                if (nextQuestion == null)
                {
                    nextQuestion = new RelayCommand(argument =>
                    {
                        States.NextQuestion(selectedQuiz);
                        currentQuiz = selectedQuiz;
                        questionout = question;
                        answerAout = answerA;
                        answerBout = answerB;
                        answerCout = answerC;
                        answerDout = answerD;
                    }, argument => true);
                }
                return nextQuestion;
            }
            set
            {
                currentQuiz = selectedQuiz;
            }

        }
        

        #endregion

    }
}
