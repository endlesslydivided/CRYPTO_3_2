using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Lab_7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        bool check = false;

        private void Calculate(object sender, RoutedEventArgs e)
        {
            if (TextX.Text != String.Empty && TextA.Text != String.Empty && TextN.Text != String.Empty)
            {
                TextX.IsReadOnly = true;
                if (check)
                {
                    TextX.Text = TextY.Text;
                }
                check = true;
                int x = Int32.Parse(TextX.Text);
                int a = Int32.Parse(TextA.Text);
                int b = Convert.ToString(x, 2).Length;
                double c = Math.Pow(2, b);
                double n = Double.Parse(TextN.Text);
                DateTime start = DateTime.Now;
                double y = Math.Abs(((a * x) + c) % n);
                DateTime finish = DateTime.Now;
                TimeSpan time = finish - start;
                TextY.Text = y.ToString();
                TextTime.Text = time.Hours.ToString() + ":" + time.Minutes.ToString() + ":" + time.Seconds.ToString() + "." + time.Milliseconds.ToString();
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
    }
}
