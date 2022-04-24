using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizRozwiazywanie
{
    internal class Question
    {
        //tresc pytania
        public string QuestionString { get; set; }
        //mozliwe odpowiedzi
        public Dictionary<char, string> Answers { get; set; }
        //czy odp prawdziwa
        public List<char> Correct { get; set; }

        public Question(string questionstring, Dictionary<char, string> answers, List<char> Correct)
        {
            this.Correct = Correct;
            this.QuestionString = questionstring;
            this.Answers = answers;
        }
        public Question()
        {
            this.Correct = new List<char>();
            this.QuestionString = string.Empty;
            this.Answers = new Dictionary<char, string>();
        }
        public override string ToString()
        {
            return $"Content: {QuestionString},\nQuestions:\n{this.getQuestionsAsString()}Correct: {this.getCorrectsAsString()}";
        }
        private string getCorrectsAsString(){
            string Corrects = "";
            foreach(char Correct in this.Correct){
                Corrects += Correct;
            }
            return Corrects;
        }
        private string getQuestionsAsString(){
            string questions = "";
            foreach(char question in this.Answers.Keys){
                questions += question + ": " + this.Answers[question] + ",\n";
            }
            return questions;
        }
        public bool CheckAnswer(char answer) => this.Answer.Contains(answer);

    }
}
