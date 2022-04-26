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
        private bool Aselected = false;
        private bool Bselected = false;
        private bool Cselected = false;
        private bool Dselected = false;
        private string startstop = "Start";
        private string next = "";
        private System.Windows.Media.Brush _borderA;
        private System.Windows.Media.Brush _borderB;
        private System.Windows.Media.Brush _borderC;
        private System.Windows.Media.Brush _borderD;
        //wyswietlane pytanie
        private string question;
        private RelayCommand nextQuestion;
        private RelayCommand startstopquiz;
        private RelayCommand questionanswerA;
        private RelayCommand questionanswerB;
        private RelayCommand questionanswerC;
        private RelayCommand questionanswerD;
        //timer
        private Timer timer;
        private int seconds;
        private int minutes;
        private string time;
        //pkt
        private int _points;
        private int _maxpoints;
        private string pointsstring;
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
        public System.Windows.Media.Brush borderA
        {
            get { return _borderA; }
            set {  _borderA = value;
                    onPropertyChanged("borderA");                
            }
        }
        public System.Windows.Media.Brush borderB
        {
            get { return _borderB; }
            set
            {   _borderB = value;
                onPropertyChanged("borderB");
            }
        }
        public System.Windows.Media.Brush borderC
        {
            get { return _borderC; }
            set
            {   _borderC = value;
                onPropertyChanged("borderC");
            }
        }
        public System.Windows.Media.Brush borderD
        {
            get { return _borderD; }
            set
            {    _borderD = value;
                 onPropertyChanged("borderD");
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
                        _points = States.CountPoints(Aselected, Bselected, Cselected, Dselected, _points, Copied);
                        States.NextQuestion(Copied);
                        currentQuiz = Copied;
                        questionout = question;
                        answerAout = answerA;
                        answerBout = answerB;
                        answerCout = answerC;
                        answerDout = answerD;
                        Aselected = false;
                        Bselected = false;
                        Cselected = false;
                        Dselected = false;
                        _borderA = System.Windows.Media.Brushes.Transparent;
                        _borderB = System.Windows.Media.Brushes.Transparent;
                        _borderC = System.Windows.Media.Brushes.Transparent;
                        _borderD = System.Windows.Media.Brushes.Transparent;
                        borderA = _borderA;
                        borderB = _borderB;
                        borderC = _borderD;
                        borderD = _borderD;

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
                            pointsstring = "pkt: " + _points.ToString() + '/' + _maxpoints.ToString();
                            seconds = 0;
                            _points = 0;
                            Timer timer2 = new Timer(1000);
                            int index = listanazw.IndexOf(quizname);
                            inWorkQuiz = quizzes[index];
                            timer2.Elapsed += OnTimedEvent;
                            //pobierz wartosc startstop, w razie startu laduj quiz w razie stopu wywal
                            BindSolve Copied = new BindSolve();
                            Copied = States.Changestartbutton(startstop, inWorkQuiz, Copied, timer2);
                            startstop = States.StartOrMenu(startstop); // zmien napis
                            next = States.Change(next, pointsstring);//zmiana napisu
                            _maxpoints = States.calculatemax(0, Copied);
                            if (startstop == "Start")//teraz zatrzymujemy quiz
                            {
                                timer.Stop();
                                time = "";
                                displayTimer = time;
                                _borderA = System.Windows.Media.Brushes.Transparent;
                                _borderB = System.Windows.Media.Brushes.Transparent;
                                _borderC = System.Windows.Media.Brushes.Transparent;
                                _borderD = System.Windows.Media.Brushes.Transparent;
                                Aselected = false;
                                Bselected = false;
                                Cselected = false;
                                Dselected = false;
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
                            borderA = _borderA;
                            borderB = _borderB;
                            borderC = _borderD;
                            borderD = _borderD;
                            Next = next;
                        }
                        catch (Exception) { //nie wybrano quizu;
                        }

                    }, argument => true);
                }
                return startstopquiz;
            }
        }
        public RelayCommand AnswerClick_A
        {
            get {
                if (questionanswerA == null)
                    questionanswerA = new RelayCommand(argument =>
                    {
                        if (Aselected == false)
                        {
                            Aselected = true;
                            _borderA = System.Windows.Media.Brushes.Gold;
                        }
                        else
                        {
                            Aselected = false;
                            _borderA = System.Windows.Media.Brushes.Transparent;
                        }
                        borderA = _borderA;
                    }, argument => true);
                return questionanswerA;
            }
        }
        public RelayCommand AnswerClick_B
        {
            get
            {
                if (questionanswerB == null)
                    questionanswerB = new RelayCommand(argument =>
                    {
                        if (Bselected == false)
                        {
                            Bselected = true;
                            _borderB = System.Windows.Media.Brushes.Gold;
                        }
                        else
                        {
                            Bselected = false;
                            _borderB = System.Windows.Media.Brushes.Transparent;
                        }
                        borderB = _borderB;
                    }, argument => true);
                return questionanswerB;
            }
        }
        public RelayCommand AnswerClick_C
        {
            get
            {
                if (questionanswerC == null)
                    questionanswerC = new RelayCommand(argument =>
                    {
                        if (Cselected == false)
                        {
                            Cselected = true;
                            _borderC = System.Windows.Media.Brushes.Gold;
                        }
                        else
                        {
                            Cselected = false;
                            _borderC = System.Windows.Media.Brushes.Transparent;
                        }
                        borderC = _borderC;
                    }, argument => true);
                return questionanswerC;
            }
        }
        public RelayCommand AnswerClick_D
        {
            get
            {
                if (questionanswerD == null)
                    questionanswerD = new RelayCommand(argument =>
                    {
                        if (Dselected == false)
                        {
                            Dselected = true;
                            _borderD = System.Windows.Media.Brushes.Gold;
                        }
                        else
                        {
                            Dselected = false;
                            _borderD = System.Windows.Media.Brushes.Transparent;
                        }
                        borderD = _borderD;
                    }, argument => true);
                return questionanswerD;
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
