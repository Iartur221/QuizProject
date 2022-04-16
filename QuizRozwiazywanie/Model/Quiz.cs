using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace QuizRozwiazywanie.Model
{
    internal class Quiz
    {
        public string Name { get; set; }
        public List<Question> questions;
        private Question current;
        private Random random;

        public Quiz(string name, List<Question> questions)
        {
            this.Name = name;
            this.questions = questions;
            string jsonVar = JsonSerializer.Serialize(this.questions);
            File.WriteAllText(this.Name + ".json", jsonVar);
        }
        public Quiz(string name)
        {
            this.Name = name;
            string jsonString = File.ReadAllText(this.Name + ".json");
            this.questions = JsonSerializer.Deserialize<List<Question>>(jsonString);
        }
        public void NextQuestion()
        {
            int rng = this.random.Next(0, this.questions.Count);
            this.current = this.questions[rng];
            this.questions.RemoveAt(rng);
        }
        public bool CheckAnswer(char answer) => this.current.Answer.Contains(answer);
        public string getCurrentContent() => this.current.Content;
        public Dictionary<char, string> getCurrentQuestions() => this.current.Questions;
    }
}