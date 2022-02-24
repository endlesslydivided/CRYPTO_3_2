using Lab2;
using Lab2.DocumentReader;
using Lab3.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> deuthAlphabet = new List<char>()
            {
                'a','b','c','d','e','f','g','h','i','j','k',
                'l','m','n','o','p','r','s','t','u','v',
                'w'
            };

            int[] keyH = new int[] { 5, 3, 6, 4, 2, 7, 1, 9, 8 };
            char[] keyHword = new char[] { 'а', 'л', 'е', 'к', 'с', 'а', 'н', 'д', 'р' };
            int[] keyV = new int[] { 6, 2, 5, 4, 3, 1 ,7};
            char[] keyVword = new char[] { 'к', 'о', 'в', 'а', 'л', 'е', 'в' };

            List<KeyValuePair<int,char>>keyVertical = new List<KeyValuePair<int, char>>();
            List<KeyValuePair<int, char>> keyHorizontal = new List<KeyValuePair<int, char>>();

            for (int i =0; i < keyV.Length;i++)
            {
                keyVertical.Add(new KeyValuePair<int,char>(keyV[i],keyVword[i]));
            }
            for (int i = 0; i < keyH.Length; i++)
            {
                keyHorizontal.Add(new KeyValuePair<int, char>(keyH[i],keyHword[i]));
            }

            try
            {
                SnakeService.MakeSnake(deuthAlphabet);
                ReplacementService.MakeReplace(deuthAlphabet,keyVertical,keyHorizontal);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");
            }


        }
    }
}
