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
        public string Content { get; set; }
        //mozliwe odpowiedzi
        public Dictionary<char, string> Questions { get; set; }
        //czy odp prawdziwa
        public List<char> Answer { get; set; }

        public Question(string content, Dictionary<char, string> questions, List<char> answer)
        {
            this.Answer = answer;
            this.Content = content;
            this.Questions = questions;
        }
        public Question()
        {
            this.Answer = new List<char>();
            this.Content = string.Empty;
            this.Questions = new Dictionary<char, string>();
        }
        public override string ToString()
        {
            return $"Content: {Content}, Questions: {this.getQuestionsAsString()}, Answer: {this.getAnswersAsString()}";
        }
        private string getAnswersAsString(){
            string answers = "";
            foreach(char answer in this.Answer){
                answers += answer;
            }
            return answers;
        }
        private string getQuestionsAsString(){
            string questions = "";
            foreach(char question in this.Questions.Keys){
                questions += question + ": " + this.Questions[question] + ", ";
            }
            return questions;
        }

    }
}
