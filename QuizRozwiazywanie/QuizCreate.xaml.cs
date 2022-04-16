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

namespace QuizRozwiazywanie
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class QuizCreate : Window
    {
        private List<int> ageList;

        public QuizCreate()
        {
            TextBoxWithErrorProvider.BrushForAll = Brushes.Red;

            InitializeComponent();

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            Quiz quiz = new Quiz("asdasdasd");

            foreach (Question question in listBox.Items)
            {
                quiz.AddQuestion(question);
            }
            quiz.saveToFile("C:/Users/Karol/Documents/GitHub/QuizProject/QuizRozwiazywanie/QuizFiles/test2.json");
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isNoEmpty(ContentTextBox) & isNoEmpty(ATextBox)& isNoEmpty(BTextBox)& isNoEmpty(CTextBox)& isNoEmpty(DTextBox))
                {
                    List<char> test = new List<char>('a');
                    Question question = new Question(ContentTextBox.Text.ToString(), prepareAnswerDict(), test);

                    listBox.Items.Add(question);

                    listBox.SelectedItem = null;

                    ContentTextBox.Text = ""; //wyczyszczenie pol po dodaniu wartości do listbox
                    ATextBox.Text = "";
                    BTextBox.Text = "";
                    CTextBox.Text = "";
                    DTextBox.Text = "";
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
                    ContentTextBox.Text = selectedValue.Content;
                    ATextBox.Text = selectedValue.Questions['a'];
                    BTextBox.Text = selectedValue.Questions['b'];
                    CTextBox.Text = selectedValue.Questions['c'];
                    DTextBox.Text = selectedValue.Questions['d'];
                }
                else
                {
                    ContentTextBox.Text = "";
                    ATextBox.Text = "";
                    BTextBox.Text = "";
                    CTextBox.Text = "";
                    DTextBox.Text = "";
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
                    selectedValue.Content = ContentTextBox.Text;
                    selectedValue.Questions = prepareAnswerDict();
                if(isNoEmpty(ContentTextBox) & isNoEmpty(ATextBox)& isNoEmpty(BTextBox)& isNoEmpty(CTextBox)& isNoEmpty(DTextBox))
                    {
                        listBox.Items.Refresh();

                        listBox.SelectedItem = null;

                        ContentTextBox.Text = "";
                        ATextBox.Text = "";
                        BTextBox.Text = "";
                        CTextBox.Text = "";
                        DTextBox.Text = "";
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
    }
}
