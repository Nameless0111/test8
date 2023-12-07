using ConsoleApp22;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Newtonsoft.Json;


namespace ConsoleApp22
{
    public class User
    {
        public string name { get; set; }
        public int min { get; set; }
        public int sec { get; set; }
    }
}

class Mein
{
    static void Main()
    {

        int count = 0;
        while (true)
        {
            string del;
            if (count != 0)
            {
                Console.WriteLine("Нажмите Enter для повтора. "); ConsoleKeyInfo key;
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);

                        Console.WriteLine("Введите имя: ");
                        del = Console.ReadLine();

                        TypeSpeedTest.Test(del);
                        break;
                }

            }
            else
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);

                Console.WriteLine("Введите имя: ");
                del = Console.ReadLine();
                count++;
                TypeSpeedTest.Test(del);
            }

        }
    }
}
class TypeSpeedTest
{

    public static void Test(string del)
    {

        bool over = false;
        string text = "Военная служба - это особый вид федеральной государственной службы, которая заключается в повседневном выполнении гражданами воинских обязанностей.\r\nОсновная задача военной службы - постоянная целенаправленная подготовка к вооруженной защите или вооруженная защита целостности и неприкосновенности территории РФ.\r\nВоенная служба в России всегда считалась почетной обязанностью, священным долгом, исключительным по важности и необходимости. Исполнение обязанностей военной службы в ВС РФ предусматривает непосредственное участие в боевых действиях, повседневную боевую подготовку, несение боевого дежурства, гарнизонной и внутренней служб, соблюдение требований воинской дисциплины.\r\n";
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
                        while (stopwatch.Elapsed.TotalMinutes <= 1)
                        {
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(0, 10);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"Таймер: {stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds:D}");
                        }
                         over = true;
                    }


                }
);

                stopwatch.Start();

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

                        mistakeCount++;
                        continue;

                    }
                }
                if (over)
                {
                    symbol = text.Length;
                }
                if (symbol == text.Length)
                {

                    stopwatch.Stop();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    Console.WriteLine("Поздравляем с прохождением!Ваш результат записан!");
                    double minut = stopwatch.Elapsed.TotalMinutes;
                    int symbolmin = (int)(correctCount / minut);
                    double second = stopwatch.Elapsed.TotalSeconds;
                    int symbolsec = (int)(correctCount / second);
                    Console.WriteLine($"Время: {stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}");
                    Console.WriteLine($"Символов/минут: {symbolmin}");
                    Console.WriteLine($"Символов/секунд: {symbolsec}");
                    var user = new User
                    {
                        name = del,
                        min = symbolmin,
                        sec = symbolsec
                    };
                    Console.Clear();
                    Table.addTable(user);
                    Table.showTable();
                    symbol = 0;
                    break;
                
        }
        break;

    }
    }
}
public static class Table
{
    private const string path = "leaderboard.json";
    private static List<User> Tabledata;
    static Table()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Tabledata = JsonConvert.DeserializeObject<List<User>>(json);
        }
        else
        {
            Tabledata = new List<User>();
        }
    }
    public static void addTable(User user)
    {
        Tabledata.Add(user);
        Tabledata = Tabledata.OrderBy(user => user.min).ToList();
        SaveLeaderboardToFile();
    }
    public static void showTable()
    {
        Console.WriteLine("Таблица рекордов: ");
        Console.WriteLine("Имя\tСкорость\tСкорость ещё");
        foreach (var user in Tabledata)
        {
            Console.WriteLine($"{user.name}\t\t{user.min}\t\t{user.sec}");
        }
    }
    private static void SaveLeaderboardToFile()
    {
        string json = JsonConvert.SerializeObject(Tabledata, Formatting.Indented);
        File.WriteAllText(path, json);
    }
}













