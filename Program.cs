using ConsoleApp22;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
namespace ConsoleApp22 { 
public class user {
        public string name { get; }
        public int time { get; set; }
        public user(string namee,double timee)
    {
            namee = name;
            timee = time;
    }
}}
class TypeSpeedTest
{

    static void Main()
    {
        int Cu = 1;
        while (Cu != 0)
        {
            Console.WriteLine("Введите имя: ");
            
           string del = Console.ReadLine();
            user  user = new user(del,0);
            string text = "ббб.";
            int symbol = 0;
            int correctCount = 0;
            int mistakeCount = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Тест на скоропечатание!\nТекст:");
            Console.WriteLine(text);
            Console.WriteLine("Нажмите клавишу Enter, чтобы начать.");
            int p = 0;
            int e = 5;
            int d = 1;


            ConsoleKeyInfo key;
            key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Console.Clear();
                    Console.WriteLine(text);
                    Thread thread = new Thread(_ =>
                {
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.White;

                        if (p == 0)
                        {
                            Console.SetCursorPosition(0, 10);
                            Console.WriteLine($"{d}: 00");
                            p++;

                            Thread.Sleep(1000);
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 10); Console.WriteLine($"0: {e}"); e--;
                            if (e == 0)
                            {
                                Console.Clear();
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                    }

                }
  );
                    int m = 0;
                    while (symbol <= text.Length)
                    {

                        if (m == 0)
                        {
                            thread.Start();
                            m++;
                        }
                        Console.SetCursorPosition(symbol, 0);

                      
                        ConsoleKeyInfo input = Console.ReadKey(true);
                        if (correctCount == 0 && mistakeCount != 0) { Console.WriteLine(text); }
                        if (input.KeyChar == text[symbol])
                        {

                            Console.SetCursorPosition(symbol, 0);
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(text[symbol]);

                            symbol++;
                            correctCount++;
                        }
                        else
                        {
                            Console.SetCursorPosition(symbol, 0);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(text[symbol]);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Clear();
                            Console.WriteLine("\nПопробуйте еще раз!");
                            symbol = 0; correctCount = 0;
                            mistakeCount++;
                            ConsoleKeyInfo key1;
                            key1 = Console.ReadKey();
                            switch (key.Key)
                            {
                                case ConsoleKey.Enter:
                                    Console.Clear();
                                    Console.WriteLine(text);
                                    break;
                            }
                            continue;

                        }
                        if (symbol == text.Length)
                        {
                            thread.Abort();
                            stopwatch.Stop();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Clear();
                            Console.WriteLine("Поздравляем с прохождением!Ваш результат записан!\nНажмите Enter чтобы пройти тест еще раз!");
                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                             
                            switch(keyInfo.Key)
                            {
                                case ConsoleKey.Enter:
                                    
                                    symbol = 0; correctCount = 0; mistakeCount = 0;
                                    Stopwatch stopwatch1 = new Stopwatch();
                                    stopwatch.Start();
                                    Console.WriteLine("Тест на скоропечатание!\nТекст:");
                                    Console.WriteLine(text);
                                    Console.WriteLine("Нажмите клавишу Enter, чтобы начать.");
                                    continue;
                                case ConsoleKey.Escape:Cu = 0;
                                    break;
                                    

                            }
                        }
                    }; break;
            }








        }
    }
}
     /*public class Tekst
      {
          void text()
          { Console.Clear();
              Console.CursorVisible = true;

              Console.WriteLine("Тест выполнен!");
              Console.WriteLine($"Затраченное время: {stopwatch.Elapsed}");
              Console.WriteLine($"Символов набрано: {correctCount}");
              Console.WriteLine($"Ошибок сделано: {mistakeCount}");

              Console.WriteLine("Нажмите любую(почти) клавишу, чтобы выйти из программы...");
              Console.ReadLine();
              Thread thread = new Thread(_ =>
              {
                  Console.ReadLine();


              }



          );
              stopwatch.Stop();
          }
      }*/


