using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace QuizRozwiazywanie
{
    public class ViewModelSolve : ChangeProperty
    {

        // lista wszystkich quizow
        private ObservableCollection<BindSolve> quizzes;
        private string[] lista;
        private ObservableCollection<string> listanazw;
        private string quizname;
        //Stan rozwiazywanego quizu
        private BindSolve inWorkQuiz;
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
        private RelayCommand startstopquiz;



        //polecenia dla trybu tworzenia

        public ViewModelSolve()
        {
            //wczytanie quizow z plikow do listy
            listanazw = new ObservableCollection<string>();
            quizzes = new ObservableCollection<BindSolve>();
            lista = Directory.GetFiles("C:/Users/artur/source/repos/QuizRozwiazywanie/QuizRozwiazywanie/QuizFiles/");
            foreach (string var in lista)
            {
                quizzes.Add(new BindSolve(var,var));
            }
            for(int i = 0; i < lista.Length; i++)
            {
                listanazw.Add((Path.GetFileName(lista[i])).Remove(Path.GetFileName(lista[i]).Length - 5));
            }
            inWorkQuiz = new BindSolve();
            selectedQuiz = new BindSolve();
            StartStop = startstop;
            currentQuiz = new BindSolve();
        }

        #region zmienne do kontroli widoku

        //bierze tylko od selected quiz, operowac na selected quiz
        public BindSolve currentQuiz
        {
            get
            {
                return currentQuiz;
            }
            set
            {
                if (answerDict.ContainsKey('a'))
                {
                    answerA = answerDict['a'];
                    answerB = answerDict['b'];
                    answerC = answerDict['c'];
                    answerD = answerDict['d'];
                    question = selectedQuiz.question;
                }
                else
                {
                    answerA = String.Empty;
                    answerB = String.Empty;
                    answerC = String.Empty;
                    answerD = String.Empty;
                    question = "Witamy w aplikacji quiz";
                }
            }
        }
        public string startstop = "Start";
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
        public string StartStop
        {
            get { return startstop; }
            set { startstop = value;
                onPropertyChanged("StartStop");
            }
        }
        public ObservableCollection<string> listaout
        {
            get { return listanazw; }
        }
        public string boundquiz
        {
            get { return quizname; }
            set { //zwraca stringa z nazwa quizu
                if(value != null)
                {
                    quizname = value;

                onPropertyChanged("boundquiz");
                }
            }
        }

        #endregion

        #region
        ///<summary>
        ///przyporzadkowanie funkcji do komendy
        ///</summary>
        public RelayCommand Next_Click
        {
            get
            {
                if (nextQuestion == null)
                {
                    nextQuestion = new RelayCommand(argument =>
                    {
                        BindSolve Copied = new BindSolve();
                        Copied = selectedQuiz;
                        States.NextQuestion(Copied);
                        currentQuiz = Copied;
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
        public RelayCommand StartStop_Click
        {
            get
            {
                if (startstopquiz == null)
                {
                    startstopquiz = new RelayCommand(argument =>
                    {
                        int index = listanazw.IndexOf(quizname);
                        inWorkQuiz = quizzes[index];
                        //pobierz wartosc startstop, w razie startu laduj quiz w razie stopu wywal
                        BindSolve Copied = new BindSolve();
                        Copied = States.Changestartbutton(startstop, inWorkQuiz, Copied);
                        string copy = States.StartOrMenu(startstop); // zmien stringa startstop
                        startstop = copy;
                        selectedQuiz = Copied;
                        currentQuiz = selectedQuiz;
                        StartStop = startstop;
                        questionout = question;
                        answerAout = answerA;
                        answerBout = answerB;
                        answerCout = answerC;
                        answerDout = answerD;

                    }, argument => true);
                }
                return startstopquiz;
            }
        }

        #endregion

    }
}
