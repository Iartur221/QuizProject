using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizRozwiazywanie.Model
{
    internal class Question
    {
        public string Content { get; set; }
        public Dictionary<char, string> Questions { get; set; }
        public List<char> Answer { get; set; }

        public Question(string content, Dictionary<char, string> questions, List<char> answer)
        {
            this.Answer = answer;
            this.Content = content;
            this.Questions = questions;
        }

        public override string ToString()
        {
            return $"Answer: {Answer}, Content: {Content}, Questions: {Questions}";
        }


    }
}
