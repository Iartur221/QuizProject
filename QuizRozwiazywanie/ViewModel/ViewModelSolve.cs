using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Timers;

namespace QuizRozwiazywanie
{
    public class ViewModelSolve : ChangeProperty
    {
        #region zmienne prywatne
        // lista wszystkich quizow
        private ObservableCollection<BindSolve> quizzes;
        private string[] lista;
        private ObservableCollection<string> listanazw;
        private string quizname;
        //Stan rozwiazywanego quizu
        private BindSolve inWorkQuiz;
        //chosen quizstate
        private BindSolve selectedQuiz;
        //buttons
        private string answerA;
        private string answerB;
        private string answerC;
        private string answerD;
        private string startstop = "Start";
        private string next = "";
        //wyswietlane pytanie
        private string question;
        private RelayCommand nextQuestion;
        private RelayCommand startstopquiz;
        //timer
        private Timer timer;
        private int seconds;
        private int minutes;
        private string time;
        #endregion

        #region konstruktor

        public ViewModelSolve()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug", "").Replace("bin\\Release", "") + "QuizFiles";
            //wczytanie quizow z plikow do listy
            listanazw = new ObservableCollection<string>();
            quizzes = new ObservableCollection<BindSolve>();
            lista = Directory.GetFiles(path);
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
            Timer timer = new Timer(1000);
            
        }
        #endregion

        #region zmienne do kontroli widoku


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
        public string Next
        {
            get { return next; }
            set
            {
                next = value;
                onPropertyChanged("Next");
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
        public string displayTimer
        {
            get { return time; }
            set 
            {
                onPropertyChanged("displayTimer");
            }
        }

        #endregion

        #region przyporzadkowanie funkcji do komendy
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
                        try {
                            seconds = 0;
                            Timer timer2 = new Timer(1000);
                            int index = listanazw.IndexOf(quizname);
                            inWorkQuiz = quizzes[index];
                            timer2.Elapsed += OnTimedEvent;
                            //pobierz wartosc startstop, w razie startu laduj quiz w razie stopu wywal
                            BindSolve Copied = new BindSolve();
                            Copied = States.Changestartbutton(startstop, inWorkQuiz, Copied, timer2);
                            startstop = States.StartOrMenu(startstop); // zmien stringa 
                            next = States.Change(next);
                            if (startstop == "Start")
                            {
                                timer.Stop();
                                time = "";
                                displayTimer = time;
                            }
                            timer = timer2;
                            selectedQuiz = Copied;
                            currentQuiz = selectedQuiz;
                            StartStop = startstop;
                            questionout = question;
                            answerAout = answerA;
                            answerBout = answerB;
                            answerCout = answerC;
                            answerDout = answerD;
                            Next = next;
                        }
                        catch (Exception) { //nie wybrano quizu;
                        }

                    }, argument => true);
                }
                return startstopquiz;
            }
        }
        public void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            seconds += 1;
            if (seconds == 59)
            {
                minutes += 1;
                time = "Czas: " + minutes.ToString() + "m" + seconds.ToString() + "s";
                seconds = 0;
            }
            else
            {
                time = "Czas: " + minutes.ToString() + "m" + seconds.ToString() + "s";
            }
            displayTimer = time;
        }

        #endregion

    }
}
