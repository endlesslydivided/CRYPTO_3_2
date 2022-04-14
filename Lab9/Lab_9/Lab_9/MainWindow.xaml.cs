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

namespace Lab_8
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

        static string base64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+ ";

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private int NOD(int first, int second)
        {
            int a, b, q, r = 1;
            if (first >= second)
            {
                a = first;
                b = second;
            }
            else
            {
                a = second;
                b = first;
            }
            while (r != 0)
            {
                q = (int)(a / b);
                r = (a % b);
                a = b;
                b = r;
            }
            return a;
        }

        static int[] sequence;
        static int[] openSequence;
        static int count;
        static int[] encText;
        static int[] decText;

        private void Generate(object sender, RoutedEventArgs e)
        {
            ComboSequence.Items.Clear();
            if (TextStartSequence.Text != String.Empty)
            {
                int start = Convert.ToInt32(TextStartSequence.Text);
                count = Convert.ToInt32(ComboCountSequence.Text);
                sequence = new int[count];
                openSequence = new int[count];
                sequence[0] = start;
                ComboSequence.Items.Add(sequence[0]);
                int sum = sequence[0];
                for (int i = 1; i < count; i++)
                {
                    sequence[i] = sum + 1;
                    sum += sequence[i];
                    ComboSequence.Items.Add(sequence[i]);
                }
                TextN.Text = (sum + 1).ToString();
            }
            else
            {
                MessageBox.Show("Вы не ввели стартовый член последовательности");
            }
        }

        private void Encrypt(object sender, RoutedEventArgs e)
        {
            if (TextA.Text != String.Empty && TextN.Text != String.Empty && TextEnc.Text != String.Empty)
            {
                int a = Convert.ToInt32(TextA.Text);
                int n = Convert.ToInt32(TextN.Text);
                if (NOD(n, a) == 1)
                {
                    for (int i = 0; i < sequence.Length; i++)
                    {
                        openSequence[i] = (a * sequence[i]) % n;
                    }

                    string text = TextEnc.Text;
                    string[] stringText = new string[text.Length];
                    if (count == 6)
                    {
                        for (int i = 0; i < text.Length; i++)
                        {
                            for (int j = 0; j < base64.Length; j++)
                            {
                                if (text[i] == base64[j])
                                {
                                    stringText[i] = Convert.ToString(j, 2).PadLeft(6, '0');
                                }
                            }
                        }
                    }
                    else if (count == 8)
                    {
                        byte[] byteAscii = Encoding.ASCII.GetBytes(text);
                        for (int i = 0; i < text.Length; i++)
                        {
                            stringText[i] = Convert.ToString(byteAscii[i], 2).PadLeft(8, '0');
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы не выбрали последовательность");
                    }

                    encText = new int[text.Length];
                    for (int i = 0; i < stringText.Length; i++)
                    {
                        int sum = 0;
                        for (int j = 0; j < openSequence.Length; j++)
                        {
                            if (stringText[i][j] == '1')
                            {
                                sum += openSequence[count - 1 - j];
                            }
                        }
                        encText[i] = sum;
                        TextDec.Text += sum + " ";
                    }
                }
                else
                {
                    MessageBox.Show("A и N не являются взаимнопростыми");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели текст или параметры");
            }
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {
            if (TextA.Text != String.Empty && TextN.Text != String.Empty && TextEnc.Text != String.Empty)
            {
                int a = Convert.ToInt32(TextA.Text);
                int n = Convert.ToInt32(TextN.Text);
                if (NOD(n, a) == 1)
                {
                    int a_ = 2;
                    while (a * a_ % n != 1)
                    {
                        a_++;
                    }
                    decText = new int[encText.Length];
                    for (int i = 0; i < encText.Length; i++)
                    {
                        decText[i] = (encText[i] * a_) % n;
                    }
                    int[,] binText = new int[decText.Length, count];
                    for (int i = 0; i < decText.Length; i++)
                    {
                        for (int j = sequence.Length - 1; j >= 0; j--)
                        {
                            if (sequence[j] <= decText[i])
                            {
                                binText[i, j] = 1;
                                decText[i] = decText[i] - sequence[j];
                            }
                            else
                            {
                                binText[i, j] = 0;
                            }
                        }
                    }
                    int[] origText = new int[decText.Length];
                    for (int i = 0; i < decText.Length; i++)
                    {
                        for (int j = 0; j < count; j++)
                        {
                            if (binText[i, j] == 1)
                            {
                                origText[i] += (int)Math.Pow(2, j);
                            }
                        }
                    }

                    string result = "";

                    if (count == 6)
                    {
                        for (int i = 0; i < origText.Length; i++)
                        {
                            result += base64[origText[i]];
                        }
                    }
                    else if (count == 8)
                    {
                        for (int i = 0; i < origText.Length; i++)
                        {
                            result += (char)origText[i];
                        }
                    }
                    TextOrig.Text = result;
                }
                else
                {
                    MessageBox.Show("A и N не являются взаимнопростыми");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели текст или параметры");
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            TextStartSequence.Clear();
            ComboSequence.Items.Clear();
            TextA.Clear();
            TextN.Clear();
            TextEnc.Clear();
            TextDec.Clear();
            TextOrig.Clear();
        }
    }
}
