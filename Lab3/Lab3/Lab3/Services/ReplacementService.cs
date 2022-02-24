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
    static class ReplacementService
    {
        public static void MakeReplace(List<char> deuthAlphabet,List<KeyValuePair<int,char>> keyVertical, List<KeyValuePair<int, char>> keyHorizontal)
        {
            EntropyChecker deuthChecker = new EntropyChecker(deuthAlphabet, 0, "Немецкий");
            string deuthText = deuthChecker.OpenDocument("deuth.txt").ReadToEnd().ToLower();
            List<char> deuthTextTrimmedList = deuthText.TakeLast(keyVertical.Count * keyHorizontal.Count).ToList();
            StringBuilder deuthTextTrimmedBuilder = new();
            foreach(char x in deuthTextTrimmedList)
            {
                deuthTextTrimmedBuilder.Append(x);
            }
            string deuthTextTrimmed = deuthTextTrimmedBuilder.ToString();

            Regex regex = new Regex(@"\W");
            deuthTextTrimmed = regex.Replace(deuthTextTrimmed, "");
            Dictionary<char, int> deuthDict = deuthChecker.alphabetListToDictionary();

            MultipleReplacement replaceEncrypter = new MultipleReplacement(deuthTextTrimmed,keyVertical,keyHorizontal);
            deuthChecker.getSymbolsCounts(deuthTextTrimmed, deuthDict);
            Dictionary<char, double> deuthChances = deuthChecker.getSymbolsChances(deuthTextTrimmed, deuthDict);

            deuthChecker.printAlphabet();
            deuthChecker.printChances(deuthChances);
            replaceEncrypter.printKeys();

            replaceEncrypter.printMatrix(replaceEncrypter.createMatrix(replaceEncrypter.Text));
            Stopwatch first = new Stopwatch();
            first.Start();
            string resultEnc = replaceEncrypter.Encrypt();
            first.Stop();
            Console.WriteLine($"Время шифрования: {first.ElapsedMilliseconds} мс \n"); ;
            replaceEncrypter.printMatrix(replaceEncrypter.createMatrix(resultEnc));

            Console.WriteLine(resultEnc);

            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo("Lab3.xlsx"));
            excel.createWorksheet("first");
            excel.addValuesFromDict(deuthChances, "first", 3);
            excel.pack.Save();
            Console.ReadKey();
        }
    }
}
