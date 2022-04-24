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
    /// Logika interakcji dla klasy SaveDialog.xaml
    /// </summary>
    public partial class SaveDialog : Window
    {
        public SaveDialog()
        {
            InitializeComponent();
        }
            public string SaveDialogText
        {
            get { return SaveDialogInput.Text; }
        }
            private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
            {
                DialogResult = true;
            }
        }
    }
