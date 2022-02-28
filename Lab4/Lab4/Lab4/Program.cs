using Lab2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
//y ≡ x + k mod N, 
//х ≡ у – k mod N
//Трисемиус - enigma
namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            string germanAlph = "abcdefghijklmnopqrstuvwxyz";
            string keyWord = "enigma";
            const string fileName = "Lab4-1.xls";
            int k = 7;
            EntropyChecker germanChecker = new EntropyChecker(germanAlph, 0, "Немецкий");
            string germanText = germanChecker.OpenDocument("german.txt").ReadToEnd().ToLower();
            Regex regex = new Regex(@"\W");
            germanText = regex.Replace(germanText, "");
            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));

            int c = 0;
            while (true)
            {
                Console.WriteLine("Введите номер задания:");
                Console.WriteLine("1- на основе соотношений при k = 7");
                Console.WriteLine("2- таблица Трисемуса, ключевое слово – enigma ");
                Console.WriteLine("3- выход");

                if (!int.TryParse(Console.ReadLine(), out c))
                {
                    c = -1;
                }
                switch (c)
                {
                    case 1:
                        {
                            EncoderK encoderK = new EncoderK(germanAlph, k);

                            Stopwatch first = new Stopwatch();
                           

                            Console.WriteLine($"Алфавит языка и алфавит для шифрования:\n{germanAlph}\n{encoderK.editedAlphabet}\n");

                            Console.WriteLine($"Текст для шифрования:\n{germanText}");

                            Dictionary<char, int> alphCounts = germanChecker.alphabetListToDictionary();
                            germanChecker.getSymbolsCounts(germanText, alphCounts);

                            Dictionary<char, double> chances = germanChecker.getSymbolsChances(germanText, alphCounts);
                            germanChecker.printChances(chances);

                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chances, "first", 0);

                            first.Start();
                            string encodedText = encoderK.encode(germanText);
                            first.Stop();
                            Console.WriteLine($"Время шифрования: {first.ElapsedMilliseconds} мс \n"); ;

                            Dictionary<char, int> alphCountsEnc = germanChecker.alphabetListToDictionary();
                            germanChecker.getSymbolsCounts(encodedText, alphCountsEnc);

                            Console.WriteLine($"Зашифрованный текст:\n{encodedText}");

                            chances = germanChecker.getSymbolsChances(germanText, alphCountsEnc);
                            germanChecker.printChances(chances);

                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chances, "first", 3);
                            excel.pack.Save();

                            Console.ReadKey();
                            Console.Clear();
                            break;

                        }

                    case 2:
                        {
                            EncoderTrisem encoderTrisem = new EncoderTrisem(germanAlph, keyWord);

                            Console.WriteLine($"Матрица для шифрования:\n");

                            encoderTrisem.printMatrix(encoderTrisem.tableAlphabet);

                            Console.WriteLine($"Текст для шифрования:\n{germanText}");

                            Dictionary<char, int> alphCounts = germanChecker.alphabetListToDictionary();
                            germanChecker.getSymbolsCounts(germanText, alphCounts);

                            Dictionary<char, double> chances = germanChecker.getSymbolsChances(germanText, alphCounts);
                            germanChecker.printChances(chances);

                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chances, "first", 6);

                            string encodedText = encoderTrisem.encode(germanText);

                            Dictionary<char, int> alphCountsEnc = germanChecker.alphabetListToDictionary();
                            germanChecker.getSymbolsCounts(encodedText, alphCountsEnc);

                            Console.WriteLine($"Зашифрованный текст:\n{encodedText}");

                            chances = germanChecker.getSymbolsChances(germanText, alphCountsEnc);
                            germanChecker.printChances(chances);

                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chances, "first", 9);
                            excel.pack.Save();

                            Console.ReadKey();
                            Console.Clear();
                            break;

                        }
                    case 3:
                        {
                            return;
                        }
                    case -1:
                        {
                            Console.Clear();
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            break;
                        }
                }
            }
        }
    }
}
