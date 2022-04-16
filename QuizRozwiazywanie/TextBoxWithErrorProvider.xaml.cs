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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizRozwiazywanie
{
    /// <summary>
    /// Logika interakcji dla klasy TextBoxWithErrorProvider.xaml
    /// </summary>
    public partial class TextBoxWithErrorProvider : UserControl
    {
        #region Wlasciwosci
        
        public static Brush BrushForAll { get; set; } = Brushes.Black;

        public Brush TextBoxBorderBrush
        {
            get { return textBoxBorder.BorderBrush; }
            set { textBoxBorder.BorderBrush = value; }
        }

        public string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        #endregion


        public TextBoxWithErrorProvider()
        {
            InitializeComponent();
            textBoxBorder.BorderBrush = BrushForAll;
        }

        public void SetError(string errorText)
        {
            textBlockToolTip.Text = errorText;
            if(errorText != "")
            {
                textBoxBorder.BorderThickness = new Thickness(1);
                toolTip.Visibility = Visibility.Visible;
            }
            else
            {
                textBoxBorder.BorderThickness = new Thickness(0);
                toolTip.Visibility = Visibility.Hidden;
            }
        }
    }
}
