using Lab2.DocumentReader;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            const string fileName = "Lab2-1.xls";

            List<char> gollandAlphabet = new List<char>()
            {
                'a','b','c','d','e','f','g','h','i','j','k',
                'l','m','n','o','p','r','s','t','u','v',
                'w'
            };

            List<char> ukranianAlphabet = new List<char>()
            {
                '\u0430','\u0431','\u0432','\u0433',
                '\u0434','\u0435','\u0454','\u0436','\u0437',
                '\u0438','\u0456','\u0457','\u0439','\u043A',
                '\u043B','\u043C','\u043D','\u043E','\u043F',
                '\u0440','\u0441','\u0442','\u0443','\u0444',
                '\u0445','\u0446','\u0447','\u0448','\u0449',
                '\u044C','\u044E','\u044F',
            };
            while (choice != 5)
            {
                Console.Clear();

                Console.WriteLine("Выберите номер задания:\n- 1\n- 2\n- 3\n- 4\n- 5-выйти");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Console.Clear();
                            EntropyChecker gollandChecker = new EntropyChecker(gollandAlphabet, 0, "Голландский");
                            EntropyChecker ukranianChecker = new EntropyChecker(ukranianAlphabet, 0, "Украинский");

                            string gollandText = gollandChecker.OpenDocument("golland.txt").ReadToEnd().ToLower();
                            string ukranianText = gollandChecker.OpenDocument("ukrainian.txt").ReadToEnd().ToLower();

                            Regex regex = new Regex(@"\W");
                            gollandText = regex.Replace(gollandText, "");
                            ukranianText = regex.Replace(ukranianText, "");

                            Dictionary<char, int> golladnDict = gollandChecker.alphabetListToDictionary();
                            Dictionary<char, int> ukranianDict = ukranianChecker.alphabetListToDictionary();

                            gollandChecker.getSymbolsCounts(gollandText, golladnDict);
                            ukranianChecker.getSymbolsCounts(ukranianText, ukranianDict);

                            Dictionary<char, double> chancesGolland = gollandChecker.getSymbolsChances(gollandText, golladnDict);
                            Dictionary<char, double> chancesUkranian = ukranianChecker.getSymbolsChances(ukranianText, ukranianDict);

                            gollandChecker.computeTextEntropy(chancesGolland);
                            ukranianChecker.computeTextEntropy(chancesUkranian);


                            gollandChecker.printAlphabet();
                            gollandChecker.printChances(chancesGolland);
                            gollandChecker.printAlhabetEntropy();


                            ukranianChecker.printAlphabet();
                            ukranianChecker.printChances(chancesUkranian);
                            ukranianChecker.printAlhabetEntropy();

                            double sumGolland = 0;
                            double sumUkranian = 0;
                            foreach (KeyValuePair<char, double> x in chancesGolland)
                            {
                                sumGolland += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesUkranian)
                            {
                                sumUkranian += x.Value;
                            }

                            Console.WriteLine($"Сумма шансов для украинского языка: {sumUkranian}");
                            Console.WriteLine($"Сумма шансов для голландского языка: {sumGolland}");

                            ExcelDocumentCreator<char,double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chancesGolland, "first", 0);
                            excel.addValuesFromDict(chancesUkranian, "first", 3);
                            excel.pack.Save();
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();

                            EntropyChecker gollandChecker = new EntropyChecker(new List<char>(){ '0', '1' }, 0, "Бинарный код");
                            EntropyChecker ukranianChecker = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код");

                            string gollandText = gollandChecker.OpenDocument("golland.txt").ReadToEnd().ToLower();
                            string ukranianText = gollandChecker.OpenDocument("ukrainian.txt").ReadToEnd().ToLower();

                            Regex regex = new Regex(@"\W");
                            gollandText = regex.Replace(gollandText, "");
                            ukranianText = regex.Replace(ukranianText, "");

                            string binTextGolland = "";
                            string binTextUkrainian = "";

                            var textChr = Encoding.UTF8.GetBytes(gollandText);
                            foreach (int chr in textChr)
                            {
                                binTextGolland += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(ukranianText);
                            foreach (int chr in textChr)
                            {
                                binTextUkrainian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> golladnDict = gollandChecker.alphabetListToDictionary();
                            Dictionary<char, int> ukranianDict = ukranianChecker.alphabetListToDictionary();

                            gollandChecker.getSymbolsCounts(binTextGolland, golladnDict);
                            ukranianChecker.getSymbolsCounts(binTextUkrainian, ukranianDict);

                            Dictionary<char, double> chancesGolland = gollandChecker.getSymbolsChances(binTextGolland, golladnDict);
                            Dictionary<char, double> chancesUkranian = ukranianChecker.getSymbolsChances(binTextUkrainian, ukranianDict);

                            gollandChecker.computeTextEntropy(chancesGolland);
                            ukranianChecker.computeTextEntropy(chancesUkranian);


                            gollandChecker.printAlphabet();
                            gollandChecker.printChances(chancesGolland);
                            gollandChecker.printAlhabetEntropy();


                            ukranianChecker.printAlphabet();
                            ukranianChecker.printChances(chancesUkranian);
                            ukranianChecker.printAlhabetEntropy();

                            double sumGolland = 0;
                            double sumUkranian = 0;
                            foreach (KeyValuePair<char, double> x in chancesGolland)
                            {
                                sumGolland += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesUkranian)
                            {
                                sumUkranian += x.Value;
                            }

                            Console.WriteLine($"Сумма шансов для украинского языка: {sumUkranian}");
                            Console.WriteLine($"Сумма шансов для голландского языка: {sumGolland}");

                            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("second");
                            excel.addValuesFromDict(chancesGolland, "second", 0);
                            excel.addValuesFromDict(chancesUkranian, "second", 3);
                            excel.pack.Save();

                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();

                            EntropyChecker gollandChecker = new EntropyChecker(gollandAlphabet, 0, "Голландский");
                            EntropyChecker ukranianChecker = new EntropyChecker(ukranianAlphabet, 0, "Украинский");
                            EntropyChecker gollandCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (голландский)");
                            EntropyChecker ukranianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (украинский)");

                            string gollandText = "kovalevalexanderaleksandrovitsj";
                            string ukranianText = "ковальоволександролександрович";

                            string binTextGolland = "";
                            string binTextUkrainian = "";

                            var textChr = Encoding.UTF8.GetBytes(gollandText);
                            foreach (int chr in textChr)
                            {
                                binTextGolland += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(ukranianText);
                            foreach (int chr in textChr)
                            {
                                binTextUkrainian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> golladnDict = gollandChecker.alphabetListToDictionary();
                            Dictionary<char, int> ukranianDict = ukranianChecker.alphabetListToDictionary();
                            Dictionary<char, int> golladnDictBin = gollandCheckerBin.alphabetListToDictionary();
                            Dictionary<char, int> ukranianDictBin = ukranianCheckerBin.alphabetListToDictionary();

                            gollandChecker.getSymbolsCounts(gollandText, golladnDict);
                            ukranianChecker.getSymbolsCounts(ukranianText, ukranianDict);
                            gollandCheckerBin.getSymbolsCounts(binTextGolland, golladnDictBin);
                            ukranianCheckerBin.getSymbolsCounts(binTextUkrainian, ukranianDictBin);

                            Dictionary<char, double> chancesGolland = gollandChecker.getSymbolsChances(gollandText, golladnDict);
                            Dictionary<char, double> chancesUkranian = ukranianChecker.getSymbolsChances(ukranianText, ukranianDict);
                            Dictionary<char, double> chancesGollandBin = gollandCheckerBin.getSymbolsChances(binTextGolland, golladnDictBin);
                            Dictionary<char, double> chancesUkranianBin = ukranianCheckerBin.getSymbolsChances(binTextUkrainian, ukranianDictBin);

                            gollandChecker.computeTextEntropy(chancesGolland);
                            ukranianChecker.computeTextEntropy(chancesUkranian);
                            gollandCheckerBin.computeTextEntropy(chancesGollandBin);
                            ukranianCheckerBin.computeTextEntropy(chancesUkranianBin);


                            gollandChecker.printAlphabet();
                            gollandChecker.printChances(chancesGolland);
                            gollandChecker.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {gollandChecker.AlphabetName}: {gollandChecker.AlphabetEntropy *gollandText.Length}");

                            ukranianChecker.printAlphabet();
                            ukranianChecker.printChances(chancesUkranian);
                            ukranianChecker.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {ukranianChecker.AlphabetName}: {ukranianChecker.AlphabetEntropy * ukranianText.Length}");

                            gollandCheckerBin.printAlphabet();
                            gollandCheckerBin.printChances(chancesGollandBin);
                            gollandCheckerBin.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {gollandCheckerBin.AlphabetName}: {gollandCheckerBin.AlphabetEntropy * binTextGolland.Length}");

                            ukranianCheckerBin.printAlphabet();
                            ukranianCheckerBin.printChances(chancesUkranianBin);
                            ukranianCheckerBin.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {ukranianCheckerBin.AlphabetName}: {ukranianCheckerBin.AlphabetEntropy * binTextUkrainian.Length}");


                            double sumGolland = 0;
                            double sumUkranian = 0;
                            double sumGollandBin = 0;
                            double sumUkranianBin = 0;
                            foreach (KeyValuePair<char, double> x in chancesGolland)
                            {
                                sumGolland += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesUkranian)
                            {
                                sumUkranian += x.Value;
                            }

                            foreach (KeyValuePair<char, double> x in chancesGollandBin)
                            {
                                sumGollandBin += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesUkranianBin)
                            {
                                sumUkranianBin += x.Value;
                            }

                            Console.WriteLine($"Сумма шансов для украинского языка: {sumUkranian}");
                            Console.WriteLine($"Сумма шансов для голландского языка: {sumGolland}");
                            Console.WriteLine($"Сумма шансов для украинского языка (бинарный): {sumUkranianBin}");
                            Console.WriteLine($"Сумма шансов для голландского языка (бинарный): {sumGollandBin}");

                            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("third");
                            excel.addValuesFromDict(chancesGolland, "third", 0);
                            excel.addValuesFromDict(chancesUkranian, "third", 3);
                            excel.addValuesFromDict(chancesGollandBin, "third", 5);
                            excel.addValuesFromDict(chancesUkranianBin, "third", 7);
                            excel.pack.Save();


                            Console.ReadKey();
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();

                            EntropyChecker gollandCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (голландский)");
                            EntropyChecker ukranianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (украинский)");

                            string gollandText = "kovalevalexanderaleksandrovitsj";
                            string ukranianText = "ковальоволександролександрович";

                            string binTextGolland = "";
                            string binTextUkrainian = "";

                            var textChr = Encoding.UTF8.GetBytes(gollandText);
                            foreach (int chr in textChr)
                            {
                                binTextGolland += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(ukranianText);
                            foreach (int chr in textChr)
                            {
                                binTextUkrainian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> golladnDictBin = gollandCheckerBin.alphabetListToDictionary();
                            Dictionary<char, int> ukranianDictBin = ukranianCheckerBin.alphabetListToDictionary();

                            gollandCheckerBin.getSymbolsCounts(binTextGolland, golladnDictBin);
                            ukranianCheckerBin.getSymbolsCounts(binTextUkrainian, ukranianDictBin);

                            Dictionary<char, double> chancesGollandBin = gollandCheckerBin.getSymbolsChances(binTextGolsland, golladnDictBin);
                            Dictionary<char, double> chancesUkranianBin = ukranianCheckerBin.getSymbolsChances(binTextUkrainian, ukranianDictBin);


                            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {gollandCheckerBin.AlphabetName}: {gollandCheckerBin.computeTextEntropyWithError(chancesGollandBin, 0.1) * binTextGolland.Length}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {gollandCheckerBin.AlphabetName}: {gollandCheckerBin.computeTextEntropyWithError(chancesGollandBin, 0.5) * binTextGolland.Length}");
                            Console.WriteLine($"Ошибка = 1. Количество информации сообщения. Язык - {gollandCheckerBin.AlphabetName}: {gollandCheckerBin.computeTextEntropyWithError(chancesGollandBin, 1) * binTextGolland.Length}");

                            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {ukranianCheckerBin.AlphabetName}: {ukranianCheckerBin.computeTextEntropyWithError(chancesUkranianBin,0.1) * binTextGolland.Length}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {ukranianCheckerBin.AlphabetName}: {ukranianCheckerBin.computeTextEntropyWithError(chancesUkranianBin, 0.5) * binTextGolland.Length}");
                            Console.WriteLine($"Ошибка = 1. Количество информации сообщения. Язык - {ukranianCheckerBin.AlphabetName}: {ukranianCheckerBin.computeTextEntropyWithError(chancesUkranianBin, 1) * binTextGolland.Length}");


                            Console.ReadKey();

                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
    }
}
