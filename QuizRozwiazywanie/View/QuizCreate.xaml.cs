using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace QuizRozwiazywanie
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class QuizCreate : Window
    {

        public QuizCreate()
        {
            TextBoxWithErrorProvider.BrushForAll = Brushes.Red;
            InitializeComponent();
            updateComboBox();
        }
        public void updateComboBox()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug", "").Replace("bin\\Release", "") + "QuizFiles";
            var listanazw = new List<string>();
            var quizzes = new List<Quiz>();
            var lista = Directory.GetFiles(path);
            comboboxquizlist.Items.Clear();
            foreach (string var in lista)
            {
                string quizName = (System.IO.Path.GetFileName(var)).Remove(System.IO.Path.GetFileName(var).Length - 5);
                quizzes.Add(new Quiz(quizName, var));
                comboboxquizlist.Items.Add(quizName);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e) {
            if (isNoEmpty(QuizNameBox))
            {
            Quiz quiz = new Quiz(QuizNameBox.Text);

            foreach (Question question in listBox.Items)
            {
                quiz.AddQuestion(question);
            }
                string path = Directory.GetCurrentDirectory().Replace("bin\\Debug", "").Replace("bin\\Release", "") + "QuizFiles\\"+QuizNameBox.Text+".json";
                quiz.saveToFile(path, new Base64Coder());
                updateComboBox();
                this.clearAll();
                this.QuizNameBox.Text = "";
                this.listBox.Items.Clear();
                
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isNoEmpty(ContentTextBox) & isNoEmpty(ATextBox)& isNoEmpty(BTextBox)& isNoEmpty(CTextBox)& isNoEmpty(DTextBox)&
                    (ACheckBox.IsChecked == true || BCheckBox.IsChecked == true || CCheckBox.IsChecked == true || DCheckBox.IsChecked == true))
                {
                    List<char> test = new List<char>('a');
                    Question question = new Question(ContentTextBox.Text.ToString(), prepareAnswerDict(), prepareGoodAnswerList());

                    listBox.Items.Add(question);

                    listBox.SelectedItem = null;

                    this.clearAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Dictionary<char,string> prepareAnswerDict()
        {
            Dictionary<char, string> answers = new Dictionary<char, string>
            {
                { 'a', ATextBox.Text },
                { 'b', BTextBox.Text },
                { 'c', CTextBox.Text },
                { 'd', DTextBox.Text }
            };
            return answers;
        }
        private List<char> prepareGoodAnswerList()
        {
            List<char> answers = new List<char>();
            if (ACheckBox.IsChecked == true)
            {
                answers.Add('a');
            }
            if (BCheckBox.IsChecked == true)
            {
                answers.Add('b');
            }
            if (CCheckBox.IsChecked == true)
            {
                answers.Add('c');
            }
            if (DCheckBox.IsChecked == true)
            {
                answers.Add('d');
            }
            return answers;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                listBox.Items.Remove(listBox.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void listBoxSelectionChanged(object sender, SelectionChangedEventArgs e) //po wybraniu wartosci w listbox wpisanie jej do pól
        {
            try
            {
                Question selectedValue = listBox.SelectedItem as Question;
                if (selectedValue != null)
                {
                    ContentTextBox.Text = selectedValue.QuestionString;
                    ATextBox.Text = selectedValue.Answers['a'];
                    BTextBox.Text = selectedValue.Answers['b'];
                    CTextBox.Text = selectedValue.Answers['c'];
                    DTextBox.Text = selectedValue.Answers['d'];
                    if (selectedValue.CheckAnswer('a'))
                    {
                        ACheckBox.IsChecked = true;
                    }
                    if (selectedValue.CheckAnswer('b'))
                    {
                        BCheckBox.IsChecked = true;
                    }
                    if (selectedValue.CheckAnswer('c'))
                    {
                        CCheckBox.IsChecked = true;
                    }
                    if (selectedValue.CheckAnswer('d'))
                    {
                        DCheckBox.IsChecked = true;
                    }

                }
                else
                {
                    this.clearAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void modifyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Question selectedValue = listBox.SelectedItem as Question;
                if (selectedValue != null)
                {
                    selectedValue.QuestionString = ContentTextBox.Text;
                    selectedValue.Answers = prepareAnswerDict();
                    selectedValue.Correct = prepareGoodAnswerList();
                if(isNoEmpty(ContentTextBox) & isNoEmpty(ATextBox)& isNoEmpty(BTextBox)& isNoEmpty(CTextBox)& isNoEmpty(DTextBox))
                    {
                        listBox.Items.Refresh();

                        listBox.SelectedItem = null;

                        this.clearAll();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool isNoEmpty(TextBoxWithErrorProvider tb)
        {
            if (tb.Text.Trim() == "")
            {
                tb.SetError("Fill in the field");
                return false;
            }
            tb.SetError("");
            return true;
        }
        private void clearAll()
        {
            ContentTextBox.Text = "";
            ATextBox.Text = "";
            BTextBox.Text = "";
            CTextBox.Text = "";
            DTextBox.Text = "";
            ACheckBox.IsChecked = false;
            BCheckBox.IsChecked = false;
            CCheckBox.IsChecked = false;
            DCheckBox.IsChecked = false;
        }

        private void comboboxquizlist_DropDownClosed(object sender, EventArgs e)
        {
            if (comboboxquizlist.SelectedItem == null) return;
            string quizName = comboboxquizlist.SelectedItem.ToString();
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug", "").Replace("bin\\Release", "") + "QuizFiles\\"+quizName+".json";
            Quiz quiz = new Quiz(quizName, path);
            QuizNameBox.Text = quizName;
            listBox.Items.Clear();
            foreach(Question question in quiz.GetQuestions())
            {
                listBox.Items.Add(question);
                listBox.SelectedItem = null;
            }
        }
    }
}
