﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace QuizRozwiazywanie
{
    internal class Quiz
    {
        public string Name { get; set; }
        //wszystkie pytania
        private List<Question> questions;
        private Question current;
        private Random random;

        public Quiz()
        {
            this.questions = new List<Question>();
            this.Name = "";
            this.current = new Question();
        }
        public Quiz(string name)
        {
            this.questions = new List<Question>();
            this.Name = name;
        }
        public Quiz(string name, string path)
        {
            this.questions = new List<Question>();
            this.Name = name;
            this.loadFromFile(path);
        }
        public void NextQuestion()
        {
            if (this.questions.Count == 0) return;
            random = new Random();
            int rng = this.random.Next(0, this.questions.Count);
            this.current = this.questions[rng];
            this.questions.RemoveAt(rng);
        }

        #region funkcje
        //sprawdz czy prawdziwe
        public bool CheckAnswer(char answer) => this.current.CheckAnswer(answer);
        //obecne pytanie
        public string getCurrentContent() => this.current.QuestionString;
        //obecne odpowiedzi
        public Dictionary<char, string> getCurrentQuestions() => this.current.Answers;
        public void AddQuestion(Question question) => this.questions.Add(question);
        public void saveToFile(string file = "")
        {
            if (file == "")
                file = this.Name + ".json";
            string jsonVar = JsonSerializer.Serialize(this.questions);
            File.WriteAllText(file, jsonVar);
        }
        public void loadFromFile(string file = "")
        {
            if (file == "")
                file = this.Name + ".json";
            string jsonVar = File.ReadAllText(file);
            this.questions = JsonSerializer.Deserialize<List<Question>>(jsonVar);
        }
        public List<Question> GetQuestions() => this.questions;
        #endregion
    }
}