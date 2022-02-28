using Lab2;
using Lab2.DocumentReader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab3.Services
{
    static class SnakeService
    {
        public static void MakeSnake(List<char> deuthAlphabet)
        {
            EntropyChecker deuthChecker = new EntropyChecker(deuthAlphabet, 0, "Немецкий");
            string deuthText = deuthChecker.OpenDocument("deuth.txt").ReadToEnd().ToLower();
            Regex regex = new Regex(@"\W");
            deuthText = regex.Replace(deuthText, "");
            Dictionary<char, int> deuthDict = deuthChecker.alphabetListToDictionary();

            SnakeEncrypter snakeEncrypter = new SnakeEncrypter(Convert.ToInt32(Math.Sqrt(deuthText.Length)), Convert.ToInt32(Math.Sqrt(deuthText.Length)), deuthText);
            deuthChecker.getSymbolsCounts(deuthText, deuthDict);
            Dictionary<char, double> deuthChances = deuthChecker.getSymbolsChances(deuthText, deuthDict);

            deuthChecker.printAlphabet();
            deuthChecker.printChances(deuthChances);

            snakeEncrypter.printMatrix(snakeEncrypter.createMatrix(snakeEncrypter.Text));
            Stopwatch first = new Stopwatch();

            first.Start();
            string resultEnc = snakeEncrypter.Encrypt();
            first.Stop();
            Console.WriteLine($"Время шифрования: {first.ElapsedMilliseconds} мс \n"); ;
           snakeEncrypter.printMatrix(snakeEncrypter.createMatrix(resultEnc));

            Console.WriteLine(resultEnc);
            char[,] resultDecr = snakeEncrypter.Decrypt(resultEnc);
            string resultDecrStr = snakeEncrypter.createString(resultDecr);

            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo("Lab3.xlsx"));
            excel.createWorksheet("first");
            excel.addValuesFromDict(deuthChances, "first", 0);
            excel.pack.Save();
            Console.ReadKey();

            Console.WriteLine("\n"+resultDecrStr);
        }
    }
}
