using System;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            int c = 0;
            while(true)
            {
                Console.WriteLine("Введите номер задания:");
                Console.WriteLine("1- НОД двух чисел");
                Console.WriteLine("2- НОД трёх чисел");
                Console.WriteLine("3- Поиск простых чисел из диапазона");

                if (!int.TryParse(Console.ReadLine(), out c))
                {
                    c = -1;
                }
                switch(c)
                {
                    case 1:
                        {
                            int x = 0, y = 0;
                            Console.Write("Введите первое число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.Write("Введите второе число: ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.WriteLine($"НОД двух чисел ({x},{y}) равен: {NODCompute.Compute(x,y)} ");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        }

                    case 2:
                        {
                            int x = 0, y = 0, z = 0;
                            Console.Write("Введите первое число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.Write("Введите второе число: ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.Write("Введите третье число: ");
                            if (!int.TryParse(Console.ReadLine(), out z))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }

                            Console.WriteLine($"НОД трёх чисел ({x},{y},{z}) равен: {NODCompute.Compute(z,NODCompute.Compute(x, y))} ");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        }
                    case 3:
                        {
                            int x = 0, y = 0;
                            Console.Write("Введите первое число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.Write("Введите второе число, больше первого: ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            NODCompute.FindSimple(x, y);
                            Console.ReadKey();
                            Console.Clear();
                            break;

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
